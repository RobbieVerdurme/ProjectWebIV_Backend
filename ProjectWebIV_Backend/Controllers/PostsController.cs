using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWebIV_Backend.DTO;
using ProjectWebIV_Backend.Models;

namespace ProjectWebIV_Backend.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Properties
        private readonly IPostRepository _postRepository;
        #endregion

        #region Constructoren
        public PostsController(IPostRepository context)
        {
            _postRepository = context;
        }
        #endregion

        #region Method
        // GET: api/Posts
        /// <summary>
        /// Get all posts ordered by name
        /// </summary>
        /// <returns>array of posts</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetAll().OrderBy(r => r.Created);
        }

        // GET: api/Posts/5
        /// <summary>
        /// Get the post with given id
        /// </summary>
        /// <param name="id">the id of the post</param>
        /// <returns>The post</returns>
        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            Post post = _postRepository.GetBy(id);
            if (post == null) return NotFound();
            return post;
        }

        // GET: api/Posts/5/2
        /// <summary>
        /// Get the comment with given id
        /// </summary>
        /// <param name="id">the id of the post</param>
        /// <param name="id2">the id of the comment</param>
        /// <returns>The comment</returns>
        [HttpGet("{id}/{id2}")]
        public ActionResult<Comment> GetComment(int id, int id2)
        {
            Post post = _postRepository.GetBy(id);
            Comment comment = post.Comments.Where(c => c.Id == id2).FirstOrDefault();
            if (comment == null) {
                return NotFound();
            }

            return comment;
        }

        // POST: api/Posts
        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="post">the new post</param>
        [Authorize(Policy = "Admin", Roles = "Admin")]
        [HttpPost]
        public ActionResult<Post> PostPost(PostDTO post)
        {
            Post postToCreate = new Post() { Title = post.Title, Description = post.Description, Img = post.Img };
            if(post.Comments != null)
            {
                foreach (var i in post.Comments)
                    postToCreate.AddComment(new Comment(i.Text, i.Name));
            }
            _postRepository.Add(postToCreate);
            _postRepository.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { id = postToCreate.Id }, postToCreate);
        }

        // POST: api/Posts/{post.id}
        /// <summary>
        /// Adds a new comment
        /// </summary>
        /// <param name="comment">the new comment</param>
        [HttpPost("{id}/comment")]
        public ActionResult<Comment> PostComment(int id, CommentDTO comment)
        {
            if(!_postRepository.TryGetPost(id, out var post))
            {
                return NotFound();
            }
            var commentToCreate = new Comment(comment.Text, comment.Name);
            post.AddComment(commentToCreate);
            _postRepository.SaveChanges();
            return _postRepository.GetComment(id, commentToCreate);
            //return CreatedAtAction("GetComment", new { id = post.Id, commentId = commentToCreate.Id }, commentToCreate);
        }

        // PUT: api/Post/5
        /// <summary>
        /// Modifies a post
        /// </summary>
        /// <param name="id">id of the post to be modified</param>
        /// <param name="post">the modified post</param>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin", Roles = "Admin")]
        public IActionResult PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            _postRepository.Update(post);
            _postRepository.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Posts/5
        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="id">the id of the post to be deleted</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin", Roles = "Admin")]
        public ActionResult<Post> DeletePost(int id)
        {
            Post post = _postRepository.GetBy(id);
            if (post == null)
            {
                return NotFound();
            }
            _postRepository.Delete(post);
            _postRepository.SaveChanges();
            return post;
        }

        // DELETE: api/Posts/6/comment
        /// <summary>
        /// Deletes a comment
        /// </summary>
        /// <param name="id">the id of the post of the comment</param>
        /// <param name="id2">the id of the comment</param>
        [HttpDelete("{id}/{id2}")]
        [Authorize(Policy = "Admin", Roles = "Admin")]
        public ActionResult<Comment> DeleteComment(int id, int id2)
        {
            Post post = _postRepository.GetBy(id);
            if (post == null)
            {
                return NotFound();
            }
            var commentToDelete = new Comment(id2);
            post.DeleteComment(commentToDelete);
            _postRepository.SaveChanges();
            return commentToDelete;
        }
        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectWebIV_Backend.DTO;
using ProjectWebIV_Backend.Models;

namespace ProjectWebIV_Backend.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
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
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetAll().OrderBy(r => r.Title);
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

        // POST: api/Posts
        /// <summary>
        /// Adds a new post
        /// </summary>
        /// <param name="post">the new post</param>
        [HttpPost]
        public ActionResult<Post> PostPost(PostDTO post)
        {
            Post postToCreate = new Post() { Title = post.Title, Description = post.Description };
            if(post.Comments != null)
            {
                foreach (var i in post.Comments)
                    postToCreate.AddComment(new Comment(i.Text, i.Name));
            }
            _postRepository.Add(postToCreate);
            _postRepository.SaveChanges();

            return CreatedAtAction(nameof(GetPost), new { id = postToCreate.Id }, postToCreate);
        }

        // PUT: api/Post/5
        /// <summary>
        /// Modifies a post
        /// </summary>
        /// <param name="id">id of the post to be modified</param>
        /// <param name="post">the modified post</param>
        [HttpPut("{id}")]
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
        #endregion
    }
}
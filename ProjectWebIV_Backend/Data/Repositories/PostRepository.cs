using Microsoft.EntityFrameworkCore;
using ProjectWebIV_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        #region Properties
        private readonly PostContext _context;
        private readonly DbSet<Post> _posts;
        #endregion

        #region Constructor
        public PostRepository(PostContext dbContext)
        {
            _context = dbContext;
            _posts = dbContext.Posts;
        }
        #endregion


        public void Add(Post post)
        {
            _posts.Add(post);
        }

        public void Delete(Post post)
        {
            _posts.Remove(post);
        }

        public IEnumerable<Post> GetAll()
        {
            return _posts.Include(r => r.Comments).ToList();
        }

        public Post GetBy(int id)
        {
            return _posts.Include(r => r.Comments).SingleOrDefault(r => r.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGetPost(int id, out Post post)
        {
            post = _context.Posts.FirstOrDefault(t => t.Id == id);
            return post != null;
        }

        public Comment GetComment(int id, Comment comment)
        {
            return _context.Posts.Where(p => p.Id == id).FirstOrDefault().Comments.Where(c => c.Name == comment.Name && c.Text == comment.Text).FirstOrDefault();
        }

        public void Update(Post post)
        {
            _context.Update(post);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectWebIV_Backend.Models
{
    public class Post
    {
        #region Properties        
        public int Id { get; set; }
        //[Required]
        public string Img { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Comment> Comments { get; set; }
        #endregion

        #region Constructor
        public Post()
        {
            Comments = new List<Comment>();
            Created = DateTime.Now;
        }

        public Post(string title): this()
        {
            Title = title;
        }

        public Post(string title, string desc) : this()
        {
            Title = title;
            Description = desc;
        }

        public Post(string title, string desc, string img) : this()
        {
            Title = title;
            Description = desc;
            Img = img;
        }
        #endregion

        #region Methods
        public void AddComment(Comment comment) => Comments.Add(comment);
        public void DeleteComment(Comment comment) {
            var commentToDelete = Comments.Where(c => c.Id == comment.Id).FirstOrDefault();
            Comments.Remove(commentToDelete);
        }
        public Comment GetComment(int id) => Comments.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}
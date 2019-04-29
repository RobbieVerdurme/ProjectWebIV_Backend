using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ProjectWebIV_Backend.Models
{
    public class Post
    {
        #region Properties
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
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
        #endregion

        #region Methods
        public void AddComment(Comment comment) => Comments.Add(comment);
        public Comment GetComment(int id) => Comments.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.Models
{
    public class Comment
    {
        #region Properties
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        [Required]
        public string Name { get; set; }
        #endregion

        #region Constructor
        public Comment(int id)
        {
            Id = id;
        }
        public Comment(int id, string text, string name)
        {
            Id = id;
            Text = text;
            Created = DateTime.Now;
            Name = name;
        }
        public Comment(string text, string name)
        {
            Text = text;
            Created = DateTime.Now;
            Name = name;
        }
        #endregion
    }
}

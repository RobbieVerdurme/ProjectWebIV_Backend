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
        public string Autheur { get; set; }
        #endregion

        #region Constructor
        public Comment(string text, string autheur)
        {
            Text = text;
            Created = DateTime.Now;
            Autheur = autheur;
        }
        #endregion
    }
}

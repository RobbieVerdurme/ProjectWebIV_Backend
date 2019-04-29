using ProjectWebIV_Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.DTO
{
    public class PostDTO
    {
        [Required]
        public string Title { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.DTO
{
    public class CommentDTO
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

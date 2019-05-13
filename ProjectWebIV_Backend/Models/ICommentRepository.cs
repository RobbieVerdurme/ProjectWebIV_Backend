using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.Models
{
    public interface ICommentRepository
    {
        Comment GetBy(int id);
        void Add(Comment comment);
        void Delete(Comment comment);
        void Update(Comment comment);
        void SaveChanges();
    }
}

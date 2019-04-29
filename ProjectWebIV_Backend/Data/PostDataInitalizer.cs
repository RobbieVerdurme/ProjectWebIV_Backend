using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebIV_Backend.Data
{
    public class PostDataInitalizer
    {
        #region Properties
        private readonly PostContext _dbContext;
        #endregion

        #region Constructor
        public PostDataInitalizer(PostContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Method
        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with Posts, see DBContext
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository :Repository<Category>, ICategory
    {
        private readonly ApllicationDbContext _db;
        public CategoryRepository(ApllicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Update(Category cat)
        {
            _db.Categories.Update(cat);
            
        }

    }
}

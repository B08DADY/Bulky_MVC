using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepo;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class Categoryrepository : Repository<Category>, ICategory
    {
        private readonly ApllicationDbContext _db;
        public Categoryrepository(ApllicationDbContext db):base(db)
        {
            _db = db;
            
        }
        public void Update(Category cat)
        {
            _db.Categories.Update(cat);
        }
    }
}

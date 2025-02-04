using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProduct
    {
        private readonly ApllicationDbContext _db;        
        public ProductRepository(ApllicationDbContext db):base(db) 
        {
            _db = db;
            
        }
        void IProduct.Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}

using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepo
{
    public interface IProduct:IRepository<Product>
    {
        public void Update(Product p);
    }
}

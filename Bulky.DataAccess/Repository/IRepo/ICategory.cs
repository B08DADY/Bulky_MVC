using Bulky.DataAccess.Repository.IRepo;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepo
{
    public interface ICategory:IRepository<Category>
    {
        public void Update(Category cat);
    }
}

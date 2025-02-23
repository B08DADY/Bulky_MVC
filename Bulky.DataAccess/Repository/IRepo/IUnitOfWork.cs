using Bulky.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepo
{
    public interface IUnitOfWork
    {
        public ICategory Category { get; }
        public IProduct Product { get; }

        public void Save();
    }
}

using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T>  where T : class  
    {
                 
        private readonly ApllicationDbContext _db;

        internal DbSet<T> entities ;
        public Repository(ApllicationDbContext db)
        {
            _db = db;
            entities = _db.Set<T>();
        }
        public void Add(T entity)
        {

            entities.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> _entitis = entities;
            _entitis = _entitis.Where(filter);
            return _entitis.FirstOrDefault();
            
        }

        public IEnumerable<T> GetAll()
        {
           IEnumerable<T> _entitis = entities;
            return _entitis;

        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            entities.RemoveRange(entity);
        }
    }
}

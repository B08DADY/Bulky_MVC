using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly ApllicationDbContext _db;
        internal DbSet<T> entities;
        public Repository(ApllicationDbContext db)
        {
            _db = db;
            entities = db.Set<T>();
            _db.Products.Include(u => u.Category);
        }
        public void Add(T cat)
        {
            entities.Add(cat);
            
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> _entities = entities;
            _entities = _entities.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _entities = _entities.Include(includeProp);

                }

            }
            return _entities.FirstOrDefault();

            
        }

        public IEnumerable<T> GetAll(string? includeProperties=null)
        {
            IQueryable<T> _entities = entities;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _entities=_entities.Include(includeProp);

                }

            }

            return _entities;
        }

        public void Remove(T cat)
        {
            entities.Remove(cat);
        }

        public void RemoveRange(IEnumerable<T> cats)
        {
            entities.RemoveRange(cats);
        }
    }
}

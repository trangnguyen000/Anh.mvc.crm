using Module.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Module.Repository
{
    public  class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T Get(object id) => _context.Set<T>().Find(id);

        public IEnumerable<T> GetAll() => _context.Set<T>().AsNoTracking().ToList();

        public IQueryable<T> GetAllAsync() => _context.Set<T>().AsNoTracking().AsQueryable();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(predicate);
        }

        public void Add(T entity) => _context.Entry<T>(entity).State = EntityState.Added;

        public void Remove(T entity) => _context.Entry<T>(entity).State = EntityState.Deleted;

        public void Update(T entity) => _context.Entry<T>(entity).State = EntityState.Modified;

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Added;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                _context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public IQueryable<T> FindAsQueryable() => _context.Set<T>().AsNoTracking().AsQueryable<T>();

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(predicate).AsQueryable();
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

    }
}

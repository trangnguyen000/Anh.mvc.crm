using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Module.Repository.Shared
{
    public interface IRepository<T> where T : class
    {
        // To get single item by its id
        T Get(object id);

        // To get all items
        IEnumerable<T> GetAll();

        IQueryable<T> GetAllAsync();

        // To search a set of items by passing expression as parameter
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        // To add a single item
        void Add(T entity);

        // To update an item
        void Update(T entity);

        // To add a set of entities
        void AddRange(IEnumerable<T> entities);

        //***
        // Returns result as Querayable such that you can apply more logic
        // on top of it
        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query();

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

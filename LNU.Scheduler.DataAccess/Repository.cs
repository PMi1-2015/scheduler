using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LNU.Scheduler.Contracts;

namespace LNU.Scheduler.DataAccess
{
    /// <summary>
    /// Abstraction for crud manipulation
    /// </summary>
    /// <typeparam name="T">entity for manipulation</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SchedulerContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SchedulerContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Adding new entity
        /// </summary>
        /// <param name="entity">new entity</param>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Removes entity by id
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        public void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);

            Delete(entityToDelete);
        }

        /// <summary>
        /// Getting all elements with type T
        /// </summary>
        /// <returns>All elements</returns>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Getting element by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>element</returns>
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">entity to update</param>
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }
    }
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LNU.Scheduler.Contracts;
using System.Linq.Expressions;
using System;

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
        /// Take items based on where condition
        /// </summary>
        /// <param name="where">where clause by which take items</param>
        /// <returns>list of items that specifies certain condition</returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
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

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entityToDelete">entity to delete</param>
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

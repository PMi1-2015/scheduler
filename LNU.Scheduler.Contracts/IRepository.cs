using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LNU.Scheduler.Contracts
{
    /// <summary>
    /// Abstraction for crud manipulation
    /// </summary>
    /// <typeparam name="T">entity for manipulation</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Getting all elements with type T
        /// </summary>
        /// <returns>All elements</returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> where);

        /// <summary>
        /// Getting element by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>element</returns>
        T GetById(int id);

        /// <summary>
        /// Adding new entity
        /// </summary>
        /// <param name="entity">new entity</param>
        void Add(T entity);

        /// <summary>
        /// Removes entity by id
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        void Delete(int id);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">entity to update</param>
        void Update(T entity);
    }
}

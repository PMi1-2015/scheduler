using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.Scheduler.Contracts
{
    public interface IUnitOfWork<T>
    {
        /// <summary>
        /// Repository for T entity
        /// </summary>
        IRepository<T> Repository { get; }

        /// <summary>
        /// Saves context
        /// </summary>
        void Save();
    }
}

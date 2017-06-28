using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDot.Infraestructure.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all developers with their skills
        /// </summary>
        /// <returns>List of developers with skills</returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}

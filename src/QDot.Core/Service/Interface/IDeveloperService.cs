using QDot.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDot.Core.Service.Interface
{
    public interface IDeveloperService
    {
        /// <summary>
        /// Get all developers with their skills
        /// </summary>
        /// <returns>List of developers with skills</returns>
        Task<IEnumerable<Developer>> GetAllAsync();

        /// <summary>
        /// Get all developers with skills above score
        /// </summary>
        /// <returns>List of developers with skills above score</returns>
        Task<IEnumerable<Developer>> GetDevelopersBySkillAsync(int skillScore);
    }
}

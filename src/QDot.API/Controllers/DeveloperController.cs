using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QDot.Infraestructure.Models;
using QDot.Core.Service.Interface;
using System;
using QDot.Infraestructure.Configuration;
using Microsoft.Extensions.Options;

namespace QDot.API.Controllers
{
    [Route("api/[controller]")]
    public class DeveloperController : Controller
    {
        private IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        /// <summary>
        /// Get all developers with their skills
        /// </summary>
        /// <returns>All developers</returns>
        [HttpGet]
        public Task<IEnumerable<Developer>> GetAsync()
        {
            return _developerService.GetAllAsync();            
        }

        /// <summary>
        /// Get developers with at least one skill with a level of {minLevel} or more. 
        /// </summary>
        /// <param name="minLevel">Minimun level of skill. If {minLevel} is not defined then all developers are returned</param>
        /// <returns>Filtered developers by skill level. It returns only the skills that are of the same type.</returns>
        [HttpGet("Skill")]
        public Task<IEnumerable<Developer>> GetDeveloperBySkillsAsync(int minLevel = 0)
        {
            //GetDevelopersBySkill calls the Get endpoint to get all developers through HTTP call on QDot.API.Client
            return _developerService.GetDevelopersBySkillAsync((int)minLevel);
        }
    }
}

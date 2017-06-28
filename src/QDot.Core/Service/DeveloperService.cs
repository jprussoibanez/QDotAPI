using QDot.Core.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QDot.Infraestructure.Models;
using QDot.Infraestructure.Repository.Interface;
using QDot.API.Client.BaseAPI;
using QDot.API.Client.QDotAPI.Requests;
using System;
using QDot.Core.Exceptions;

namespace QDot.Core.Service
{
    public class DeveloperService : IDeveloperService
    {
        #region Attributes

        private IRepository<Developer> _repository;
        private IAPIClient _apiClient;

        #endregion

        #region Constructors

        public DeveloperService(IRepository<Developer> repository, IAPIClient apiClient)
        {
            _repository = repository;
            _apiClient = apiClient;
        }

        #endregion

        #region Operations

        public Task<IEnumerable<Developer>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Developer>> GetDevelopersBySkillAsync(int skillLevel = 0)
        {
            if (skillLevel < 0)
            {
                throw new ServiceParameterException("Skill level cannot be negative");
            }

            var allDevelopers = await _apiClient.Execute(new GetAllDevelopersRequest());

            return (from developer in allDevelopers
                    let types = developer.Skills.Where(s => s.Level >= skillLevel).Select(s => s.Type).Distinct().ToList()
                    where types.Count > 0
                    let skills = developer.Skills.Where(s => types.Contains(s.Type)).ToList()
                    select new Developer()
                    {
                        Age = developer.Age,
                        FirstName = developer.FirstName,
                        LastName = developer.LastName,
                        Skills = skills
                    });
        }

        #endregion
    }
}

using Newtonsoft.Json;
using QDot.Infraestructure.Models;
using QDot.Infraestructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using QDot.Infraestructure.Configuration;

namespace QDot.Infraestructure.Repository
{
    public class DeveloperRepository : IRepository<Developer>
    {
        private readonly InfraestructureSettings _infraestructureSettings;

        public DeveloperRepository(IOptions<InfraestructureSettings> optionsAccessor)
        {
            _infraestructureSettings = optionsAccessor.Value;
        }

        #region Operations
        /// <summary>
        /// Get all developers
        /// </summary>
        /// <returns>Developers with skills</returns>
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            var jsonText = await _GetJsonDevelopersAsync();

            //Json.NET does not have a real async method
            var allDevelopers = JsonConvert.DeserializeObject<List<Developer>>(jsonText);

            return allDevelopers;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Read developers json file
        /// </summary>
        /// <returns>Json file with developer data</returns>
        private async Task<string> _GetJsonDevelopersAsync()
        {
            //TODO: Remove static dependency on File when IO abstractions is nuget available for dotnet core for testability
            //https://github.com/joaope/System.IO.Abstractions.Core
            using (var reader = File.OpenText(_infraestructureSettings.DeveloperPath))
            {
                return await reader.ReadToEndAsync();
            }
        }
        #endregion
    }
}
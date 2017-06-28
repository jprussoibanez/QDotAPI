using Microsoft.Extensions.DependencyInjection;
using QDot.API.Client.BaseAPI;
using QDot.API.Client.QDotAPI;
using QDot.Core.Service;
using QDot.Core.Service.Interface;
using QDot.Infraestructure.Models;
using QDot.Infraestructure.Repository;
using QDot.Infraestructure.Repository.Interface;

namespace QDot.Core.DependencyInjection
{
    public static class ServiceLoader
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDeveloperService, DeveloperService>();
            services.AddSingleton<IRepository<Developer>, DeveloperRepository>();
            services.AddSingleton<IAPIClient, QDotAPIClient>();
        }
    }
}

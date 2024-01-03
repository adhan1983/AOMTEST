using AOMTEST.HttpRequest;
using AOMTEST.HttpRequest.Interfaces;
using AOMTEST.Logger;
using AOMTEST.Logger.Interfaces;
using AOMTEST.Repository;
using AOMTEST.Repository.Interfaces;
using AOMTEST.Services;
using AOMTEST.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AOMTEST.ExtensionsMethods
{
    public static class ServicesExtensions
    {
        public static ServiceCollection AddingDependenciasService(this ServiceCollection services) 
        {
            services.AddTransient<ISecurityService, SecurityService>();

            services.AddTransient<ISecurityPriceHttp, SecurityPriceHttp>();

            services.AddTransient<ISecurityRepository, SecutiryRepository>();

            services.AddSingleton<ILoggerSecurityService, LoggerSecurityService>();

            return services;

        }
    }
}

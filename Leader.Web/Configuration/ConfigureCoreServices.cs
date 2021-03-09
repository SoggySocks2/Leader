using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smeat.Leader.Infrastructure.Data;
using Smeat.Leader.Infrastructure.Services;

namespace Smeat.Leader.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}

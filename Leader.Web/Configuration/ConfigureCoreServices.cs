using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smeat.Leader.Infrastructure.Data;
using Smeat.Leader.Infrastructure.Data.Config;
using Smeat.Leader.Infrastructure.Services;

namespace Smeat.Leader.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //services.AddScoped<EntityTypeBuilder<AddressConfiguration>, AddressConfiguration>();
            services.AddTransient<AddressConfiguration>();

            return services;
        }
    }
}

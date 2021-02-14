using Microsoft.AspNetCore.Hosting;

//[assembly: HostingStartup(typeof(Microsoft.eShopWeb.Web.Areas.Identity.IdentityHostingStartup))]
namespace Smeat.Leader.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}

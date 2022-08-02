using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MemoProject.Areas.Identity.IdentityHostingStartup))]
namespace MemoProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
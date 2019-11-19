using System;
using cs4540_final_project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(cs4540_final_project.Areas.Identity.IdentityHostingStartup))]
namespace cs4540_final_project.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserRolesDB>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserRolesDBConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<UserRolesDB>();
            });
        }
    }
}
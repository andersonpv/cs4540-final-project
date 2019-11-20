using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using cs4540_final_project.Data;
using cs4540_final_project.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace cs4540_final_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                StoreContext storeContext = services.GetRequiredService<StoreContext>();
                DbInitializer.InitializeAsync(storeContext, services).Wait();

                UserRolesDB roleContext = services.GetRequiredService<UserRolesDB>();
                DbInitializer.InitializeAsync(roleContext, services).Wait();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

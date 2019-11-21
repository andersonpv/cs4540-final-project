using cs4540_final_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cs4540_final_project.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(StoreContext StoreContext, UserRolesDB UserContext, IServiceProvider serviceProvider)
        {
            UserContext.Database.Migrate();

            // Initialize user database
            if (UserContext.Users.Any())
            {
                return;   // DB has been seeded
            }

            RoleManager<IdentityRole> RoleManager = serviceProvider
               .GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles
            string[] roleNames = { "Admin", "Barber", "Customer" };
            IdentityResult roleResult;
            foreach (string roleName in roleNames)
            {
                bool roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            UserManager<IdentityUser> userManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            string UserPassword = "123ABC!@#def";

            // Create user Barber01
            IdentityUser barber = new IdentityUser
            {
                Id = "1",
                UserName = "Barber@RazorSharp.com",
                NormalizedUserName = "BARBER@RAZORSHARP.COM",
                Email = "Barber@RazorSharp.com",
                NormalizedEmail = "BARBER@RAZORSHARP.COM",
                EmailConfirmed = true,
            };

            IdentityResult createUser = await userManager.CreateAsync(barber, UserPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(barber, "Barber");
            }

            // Create user Customer
            IdentityUser customer = new IdentityUser
            {
                Id = "2",
                UserName = "Customer@gmail.com",
                NormalizedUserName = "CUSTOMER@GMAIL.COM",
                Email = "Customer@gmail.com",
                NormalizedEmail = "CUSTOMER@GMAIL.COM",
                EmailConfirmed = true,
            };

            IdentityResult createUser2 = await userManager.CreateAsync(customer, UserPassword);
            if (createUser2.Succeeded)
            {
                await userManager.AddToRoleAsync(customer, "Customer");
            }

            // Create user Admin
            customer = new IdentityUser
            {
                Id = "3",
                UserName = "Admin@RazorSharp.com",
                NormalizedUserName = "ADMIN@RAZORSHARP.COM",
                Email = "Admin@RazorSharp.com",
                NormalizedEmail = "ADMIN@RAZORSHARP.COM",
                EmailConfirmed = true,
            };

            IdentityResult createUser3 = await userManager.CreateAsync(customer, UserPassword);
            if (createUser3.Succeeded)
            {
                await userManager.AddToRoleAsync(customer, "Admin");
            }


            // Initialize Store Database

            WorkerComment comment = new WorkerComment()
            {
                Comment = "Test Comment",
                LastUpdated = DateTime.UtcNow.ToLocalTime(),
            };

            Worker worker = new Worker()
            {
                User = barber,
                Job = "Barber",
                Name = "Bob Smith",
                Services = "Haircut, Bear Trim, Lineup",
                WorkerComments = new List<WorkerComment>() { comment },
            };

            StoreContext.Worker.Add(worker);

            StoreContext.SaveChanges();
        }
    }
}

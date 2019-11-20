using cs4540_final_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cs4540_final_project.Data
{
    public class DbInitializer
    {

        public static async Task InitializeAsync(UserRolesDB context, IServiceProvider serviceProvider)
        {
            context.Database.Migrate();

            if (context.Users.Any())
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
            IdentityUser user = new IdentityUser
            {
                Id = "1",
                UserName = "Barber@RazorSharp.com",
                NormalizedUserName = "BARBER01@RAZORSHARP.COM",
                Email = "Barber01@RazorSharp.com",
                NormalizedEmail = "BARBER01@RAZORSHARP.COM",
                EmailConfirmed = true,
            };

            IdentityResult createUser = await userManager.CreateAsync(user, UserPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Barber");
            }

            // Create user Customer
            user = new IdentityUser
            {
                Id = "2",
                UserName = "Customer01@gmail.com",
                NormalizedUserName = "BARBER02@RAZORSHARP.COM",
                Email = "Barber02@RazorSharp.com",
                NormalizedEmail = "BARBER02@RAZORSHARP.COM",
                EmailConfirmed = true,
            };

            IdentityResult createUser2 = await userManager.CreateAsync(user, UserPassword);
            if (createUser2.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Customer");
            }

            // Create user Admin
            user = new IdentityUser
            {
                Id = "3",
                UserName = "Admin@RazorSharp.com",
                NormalizedUserName = "ADMIN@RAZORSHARP.COM",
                Email = "admin@RazorSharp.com",
                NormalizedEmail = "ADMIN@RAZORSHARP.COM",
                EmailConfirmed = true,
            };

            IdentityResult createUser3 = await userManager.CreateAsync(user, UserPassword);
            if (createUser3.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}

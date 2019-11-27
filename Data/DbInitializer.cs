using cs4540_final_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cs4540_final_project.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(UserRolesDB context, WorkerContext workerContext, IServiceProvider serviceProvider)
        {
            //context.Database.EnsureDeleted();
            //workerContext.Database.EnsureDeleted();

            context.Database.Migrate();
            workerContext.Database.Migrate();

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
                NormalizedUserName = "BARBER@RAZORSHARP.COM",
                Email = "Barber@RazorSharp.com",
                NormalizedEmail = "BARBER@RAZORSHARP.COM",
                EmailConfirmed = true,
            };
            Worker barber01 = new Worker()
            {
                Name = "Dan Smith",
                Services = "Haircuts, Beards",
                Description = "Barber for 8 years professionally.",
                User = user,
                Job = "Barber",
                Schedule = new List<DaySchedule>
                {
                    new DaySchedule { dateTime = new DateTime(2019, 11, 20), Nine = true, NineThirty = true, Ten = true },
                    new DaySchedule { dateTime = new DateTime(2019, 12, 5), Twelve = true, TwelveThirty = true, One = true },
                    new DaySchedule { dateTime = new DateTime(2018, 1, 5), Four = true, FourThirty = true }
                }
            };
            workerContext.Add(barber01);
            workerContext.SaveChanges();
            IdentityResult createUser = await userManager.CreateAsync(user, UserPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Barber");
            }


            // Create user Barber02
            IdentityUser user2 = new IdentityUser
            {
                Id = "2",
                UserName = "Barber2@RazorSharp.com",
                NormalizedUserName = "BARBER2@RAZORSHARP.COM",
                Email = "Barber2@RazorSharp.com",
                NormalizedEmail = "BARBER2@RAZORSHARP.COM",
                EmailConfirmed = true,
            };
            Worker barber02 = new Worker()
            {
                Name = "Ben Jones",
                Services = "Haircuts",
                Description = "Barber for 2 years.",
                User = user2,
                Job = "Barber",
                Schedule = new List<DaySchedule>
                {
                    new DaySchedule { dateTime = new DateTime(2019, 11, 20), Nine = true, NineThirty = true, Ten = true },
                    new DaySchedule { dateTime = new DateTime(2019, 12, 5), Twelve = true, TwelveThirty = true, One = true },
                    new DaySchedule { dateTime = new DateTime(2018, 1, 5), Four = true, FourThirty = true }
                }
            };
            workerContext.Add(barber02);
            workerContext.SaveChanges();
            IdentityResult createUser4 = await userManager.CreateAsync(user2, UserPassword);
            if (createUser4.Succeeded)
            {
                await userManager.AddToRoleAsync(user2, "Barber");
            }


            // Create user Barber03
            IdentityUser user3 = new IdentityUser
            {
                Id = "3",
                UserName = "Barber3@RazorSharp.com",
                NormalizedUserName = "BARBER3@RAZORSHARP.COM",
                Email = "Barber3@RazorSharp.com",
                NormalizedEmail = "BARBER3@RAZORSHARP.COM",
                EmailConfirmed = true,
            };
            Worker barber03 = new Worker()
            {
                Name = "Samantha Harris",
                Services = "Haircuts/hair styling, beards, nails",
                Description = "Barber for 9 years.",
                User = user3,
                Job = "Barber, Hairstylist",
                Schedule = new List<DaySchedule>
                {
                    new DaySchedule { dateTime = new DateTime(2019, 11, 22), Nine = true, NineThirty = true, Ten = true, ElevenThirty = true},
                    new DaySchedule { dateTime = new DateTime(2019, 12, 5), Twelve = true },
                    new DaySchedule { dateTime = new DateTime(2018, 1, 5), Four = true, FourThirty = true }
                }
            };
            workerContext.Add(barber03);
            workerContext.SaveChanges();
            IdentityResult createUser5 = await userManager.CreateAsync(user3, UserPassword);
            if (createUser5.Succeeded)
            {
                await userManager.AddToRoleAsync(user3, "Barber");
            }

            WorkerComment GoodComment = new WorkerComment()
            {
                Comment = "Good Job!",
                StarRating = 5,
                Worker = barber01,
                LastUpdated = DateTime.UtcNow.ToLocalTime(),
            };

            WorkerComment DecentComment = new WorkerComment()
            {
                Comment = "Decent Job.",
                StarRating = 3,
                Worker = barber01,
                LastUpdated = DateTime.UtcNow.ToLocalTime(),
            };

            WorkerComment TerribleComment = new WorkerComment()
            {
                Comment = "Terrible Job.",
                StarRating = 1,
                Worker = barber01,
                LastUpdated = DateTime.UtcNow.ToLocalTime(),
            };

            // Create user Customer
            user = new IdentityUser
            {
                Id = "4",
                UserName = "Customer@gmail.com",
                NormalizedUserName = "CUSTOMER@GMAIL.COM",
                Email = "Customer@gmail.com",
                NormalizedEmail = "CUSTOMER@GMAIL.COM",
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
                Id = "5",
                UserName = "Admin@RazorSharp.com",
                NormalizedUserName = "ADMIN@RAZORSHARP.COM",
                Email = "Admin@RazorSharp.com",
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

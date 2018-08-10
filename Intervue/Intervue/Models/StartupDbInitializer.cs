using Intervue.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intervue.Models
{
    public class StartupDbInitializer
    {
        /// <summary>
        /// This is to assign roles via Identity. One is for Admin while the other is for a Member.
        /// </summary>
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole {
                                 Name = ApplicationUserRoles.Admin,
                                 NormalizedName = ApplicationUserRoles.Admin.ToUpper(),
                                 ConcurrencyStamp = Guid.NewGuid().ToString()
                             },
            new IdentityRole {
                                 Name = ApplicationUserRoles.Member,
                                 NormalizedName = ApplicationUserRoles.Member.ToString(),
                                 ConcurrencyStamp = Guid.NewGuid().ToString()
                             }
        };

        /// <summary>
        /// This will seed data into the database. In this case, it is for storing the users.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="userManager"></param>
        public static async void SeedDataAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            //This will doublecheck to see if what was created in the database is present
            await dbContext.Database.EnsureCreatedAsync();
            await AddRolesAsync(dbContext);
        }

        /// <summary>
        /// This adds roles into the database.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private static async Task AddRolesAsync(ApplicationDbContext dbContext)
        {
            if (dbContext.Roles.Any()) { return; }

            foreach (var role in Roles)
            {
                await dbContext.Roles.AddAsync(role);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
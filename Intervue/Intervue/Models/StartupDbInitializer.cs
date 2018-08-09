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

        public static async void SeedDataAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            await dbContext.Database.EnsureCreatedAsync();
            await AddRolesAsync(dbContext);
        }

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

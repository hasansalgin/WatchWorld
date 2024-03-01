using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppIdentityDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync(AuthorizationConstants.Roles.ADMINISTRATOR))
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATOR));
            }

            if (!await userManager.Users.AnyAsync(x => x.Email == AuthorizationConstants.DEFAULT_ADMIN_USER))
            {
                var adminUser = new ApplicationUser()
                {
                    Email = AuthorizationConstants.DEFAULT_ADMIN_USER,
                    UserName = AuthorizationConstants.DEFAULT_ADMIN_USER,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATOR);
            }

            if (!await userManager.Users.AnyAsync(x => x.Email == AuthorizationConstants.DEFAULT_DEMO_USER))
            {
                var demoUser = new ApplicationUser()
                {
                    Email = AuthorizationConstants.DEFAULT_DEMO_USER,
                    UserName = AuthorizationConstants.DEFAULT_DEMO_USER,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(demoUser, AuthorizationConstants.DEFAULT_PASSWORD);
            }
        }

    }
}

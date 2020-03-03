using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoleBasedSecurityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedSecurityApp.Data
{
    public class Seed
    {
        public static async Task PopulateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "ProjectManager", "Developer", "Submitter",
            "Demo_Admin", "Demo_ProjectManager", "Demo_Developer", "Demo_Submitter" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //await CreateSuperUser(serviceProvider, configuration);
        }

        private static async Task CreateSuperUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var superUser = new ApplicationUser
            {
                UserName = configuration.GetSection("AppSettings")["UserName"],
                Email = configuration.GetSection("AppSettings")["UserEmail"]
            };

            string userPassword = configuration.GetSection("AppSettings")["UserPassword"];
            ApplicationUser user = await userManager.FindByEmailAsync(configuration.GetSection("AppSettings")["UserEmail"]);

            if (user == null)
            {
                IdentityResult createSuperUser = await userManager.CreateAsync(superUser, userPassword);
                if (createSuperUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");
                    Console.WriteLine("SuperUser created");
                }
                else
                {
                    Console.WriteLine("SuperUser not created");
                }
            }
        }
    }
}

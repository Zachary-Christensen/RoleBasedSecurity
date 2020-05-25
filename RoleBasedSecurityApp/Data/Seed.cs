using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoleBasedSecurityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleBasedSecurityApp.Data
{
    public class Seed
    {
        public static async Task PopulateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            List<string> roleNames = new List<string> { "Admin", "ProjectManager", "Developer", "Submitter",
            "Demo_Admin", "Demo_ProjectManager", "Demo_Developer", "Demo_Submitter" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            await CreateDemoUsers(serviceProvider, configuration, roleNames.FindAll(
                delegate (string role)
                {
                    return role.StartsWith("Demo");
                }));
            //await CreateSuperUser(serviceProvider, configuration);
        }

        private static async Task CreateDemoUsers(IServiceProvider serviceProvider, IConfiguration configuration, List<string> demoRoleNames)
        {
            var password = "DemoUser1!";

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var roleName in demoRoleNames)
            {
                var demoUser = new ApplicationUser
                {
                    UserName = roleName + "@email.com",
                    Email = roleName + "@email.com"
                };

                ApplicationUser user = await userManager.FindByEmailAsync(demoUser.Email);

                if (user == null)
                {
                    IdentityResult createSuperUser = await userManager.CreateAsync(demoUser, password);
                    if (createSuperUser.Succeeded)
                    {
                        await userManager.AddToRoleAsync(demoUser, roleName);
                        await userManager.AddToRoleAsync(demoUser, "Demo_Submitter");
                        Console.WriteLine(roleName + " created");
                    }
                    else
                    {
                        Console.WriteLine(roleName + " not created");
                    }
                }
            }




            
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

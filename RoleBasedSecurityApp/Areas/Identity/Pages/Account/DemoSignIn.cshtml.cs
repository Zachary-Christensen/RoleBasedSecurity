using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RoleBasedSecurityApp.Models;

namespace RoleBasedSecurityApp.Areas.Identity.Pages.Account
{
    public class DemoSignInModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DemoSignInModel> _logger;
        private readonly IEmailSender _emailSender;

        public DemoSignInModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<DemoSignInModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }

        }

        public void PopulateRoles()
        {
            ViewData["Roles"] = _roleManager.Roles.ToList().FindAll(
                delegate (IdentityRole role1)
                {
                    return role1.Name.StartsWith("Demo");
                });
        }

        public void OnGet()
        {
            PopulateRoles();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            //var user = await _userManager.get//.GetUsersInRoleAsync(Input.Role) as ApplicationUser;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Role + "@email.com", "DemoUser1!", false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                } 
            }
            
            //await _signInManager.SignInAsync(user, isPersistent: false);
            //await _signInManager.SignInAsync(user, isPersistent: false);



            PopulateRoles();
            return Page();
        }
    }
}

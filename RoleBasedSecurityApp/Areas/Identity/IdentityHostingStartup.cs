using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoleBasedSecurityApp.Data;
using RoleBasedSecurityApp.Models;

[assembly: HostingStartup(typeof(RoleBasedSecurityApp.Areas.Identity.IdentityHostingStartup))]
namespace RoleBasedSecurityApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
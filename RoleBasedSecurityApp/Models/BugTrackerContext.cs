using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedSecurityApp.Models
{
    public class BugTrackerContext : DbContext
    {

        public BugTrackerContext(DbContextOptions<BugTrackerContext> options) 
            : base(options)
        { }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCounter> ProjectCounters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectCounter>().HasKey(table => new { table.ProjectID, table.CounterID });
        }
    }
}

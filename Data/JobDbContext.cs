using System;
using Microsoft.EntityFrameworkCore;
using TechJobsAuthentication.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TechJobsAuthentication.Data
{
    public class JobDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Job> Jobs { get; set; }

        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

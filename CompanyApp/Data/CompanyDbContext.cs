using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CompanyApp.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) 
            : base(options) { }

        public DbSet<Employee> Employee { get; set; }
        //public DbSet<Report> Report { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Point> Point { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one -- report to employee
            //modelBuilder.Entity<Report>().HasOne(r => r.Employee);

            //many to one -- employees to department
            modelBuilder.Entity<Employee>().HasOne(e => e.Department).WithMany(d => d.Employee);

            //many to many -- employees to projects
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(t => new { t.EmployeeId, t.ProjectId });

            // many employees one project
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(e => e.Project)
                .WithMany(ep => ep.ProjectEmployee)
                .HasForeignKey(e => e.ProjectId);

            //many projects one employee
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(p => p.Employee)
                .WithMany(pe => pe.EmployeeProject)
                .HasForeignKey(p => p.EmployeeId);

            //many to one -- employees to role
            //modelBuilder.Entity<Employee>().HasOne(e => e.IdentityRole).WithMany(r => r.User);

            //many to one -- points to employee
            modelBuilder.Entity<Point>().HasOne(p => p.Employee).WithMany(e => e.Point);
            modelBuilder.Entity<Point>().HasOne(p => p.ReceivedFrom);
        }

        public DbSet<CompanyApp.Models.SelectEmployeeViewModel> SelectEmployeeViewModel { get; set; }
    }

    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        { }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackYourExpensesApp.Models;

namespace TrackYourExpensesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<FixedExpenses>().HasBaseType<Expenses>();
            builder.Entity<VariableExpenses>().HasBaseType<Expenses>();

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<VariableExpenses> VariableExpenses { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<TrackYourExpensesApp.Models.FixedExpenses> FixedExpenses { get; set; }
    }
}

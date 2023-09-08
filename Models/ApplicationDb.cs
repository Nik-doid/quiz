using Microsoft.EntityFrameworkCore;
using MIS.Models; // Make sure to replace with your actual User model namespace
using MIS.Models.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MIS.Controllers
{

    public class ApplicationDb : DbContext
    {

        public DbSet<User> Users { get; set; } // Replace with your User model

        // Constructor
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        {

        }

        // You can also add other DbSet properties for your other entities

        // Override OnModelCreating if you need to configure entity relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.Entity<Answer>().HasNoKey();

            // Add your entity configurations here
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}


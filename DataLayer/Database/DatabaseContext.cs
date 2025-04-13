using Microsoft.EntityFrameworkCore;
using DataLayer.Model;
using Welcome.Others;

namespace DataLayer.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseUser>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<LogEntry>()
                .HasKey(e => e.Id);

            // Seed data
            var user = new DatabaseUser
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                Role = UserRole.ADMIN,
                Expires = DateTime.Now.AddYears(1)
            };

            var user2 = new DatabaseUser
            {
                Id = 2,
                Username = "student",
                Password = "student",
                Role = UserRole.STUDENT,
                Expires = DateTime.Now.AddYears(1)
            };

            modelBuilder.Entity<DatabaseUser>().HasData(user, user2);
        }
    }
} 
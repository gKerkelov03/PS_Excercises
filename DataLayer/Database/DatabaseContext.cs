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
        public DbSet<Discipline> Disciplines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DatabaseUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FacultyNumber).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.FirstName);
                entity.Property(e => e.LastName);
                entity.Property(e => e.PhoneNumber);
                entity.Property(e => e.Department);
                entity.Property(e => e.DateOfBirth);
                entity.Property(e => e.Address);
                entity.Property(e => e.CreatedAt);
                entity.Property(e => e.LastLogin);
                entity.Property(e => e.IsActive);
                entity.Property(e => e.ProfilePicturePath);
            });

            modelBuilder.Entity<LogEntry>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Discipline>()
                .HasKey(e => e.Id);
        }
    }
} 
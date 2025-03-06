using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Welcome.Others;

namespace DataLayer.Database;

public class DatabaseContext : DbContext
{
  public DbSet<DatabaseUser> Users { get; set; }
  
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    var solutionFolder =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    var databaseFile = "welcome.db";
    var databasePath = Path.Combine(solutionFolder, databaseFile);
    optionsBuilder.UseSqlite($"Data Source={databasePath}");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<DatabaseUser>().Property(e => e.Id).ValueGeneratedOnAdd();
    var user = new DatabaseUser()
    {
      Id = 1,
      Name = "John Doe",
      Password = "1234",
      Role = UserRole.ADMIN,
      Expires = DateTime.Now.AddYears(2)
    };
    
    var user2 = new DatabaseUser()
    {
      Id = 2,
      Name = "Doe Min",
      Password = "1234",
      Role = UserRole.STUDENT,
      Expires = DateTime.Now.AddYears(2)
    };
    
    modelBuilder.Entity<DatabaseUser>().HasData(user, user2);
  }
}
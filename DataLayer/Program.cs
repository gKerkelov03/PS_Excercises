using DataLayer.Database;
using DataLayer.Model;
using Welcome.Others;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        var solutionFolderName = "PS_Excercises";
        var databaseFileName = "database.db";
        var documentsFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        var solutionFolder = System.IO.Path.Combine(documentsFolderPath, solutionFolderName);
        var databasePath = System.IO.Path.Combine(solutionFolder, databaseFileName);
        optionsBuilder.UseSqlite($"Data Source={databasePath}");
        
        using var context = new DatabaseContext(optionsBuilder.Options);
        context.Database.EnsureCreated();
        
        var logger = new DataLayer.Logger(context);
        
        while (true)
        {
            Console.WriteLine("\nUser Management Menu:");
            Console.WriteLine("1. List all users");
            Console.WriteLine("2. Add new user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. List all logs");
            Console.WriteLine("5. Exit");
            Console.Write("\nEnter your choice (1-5): ");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ListAllUsers(context, logger);
                    break;
                case "2":
                    AddNewUser(context, logger);
                    break;
                case "3":
                    DeleteUser(context, logger);
                    break;
                case "4":
                    ListAllLogs(context);
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    
    static void ListAllUsers(DatabaseContext context, DataLayer.Logger logger)
    {
        var users = context.Users.ToList();
        Console.WriteLine("\nAll Users:");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Name: {user.Username}, Role: {user.Role}");
        }
        logger.LogInfo("Retrieved all users");
    }
    
    static void AddNewUser(DatabaseContext context, DataLayer.Logger logger)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        
        var newUser = new DatabaseUser
        {
            Username = username,
            Password = password,
            Role = UserRole.STUDENT,
            Expires = DateTime.Now.AddYears(1)
        };
        
        context.Users.Add(newUser);
        context.SaveChanges();
        
        logger.LogInfo($"Added new user: {username}");
        Console.WriteLine("User added successfully!");
    }
    
    static void DeleteUser(DatabaseContext context, DataLayer.Logger logger)
    {
        Console.Write("Enter username to delete: ");
        var username = Console.ReadLine();
        
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            logger.LogInfo($"Deleted user: {username}");
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine("User not found!");
            logger.LogWarning($"Attempted to delete non-existent user: {username}");
        }
    }
    
    static void ListAllLogs(DatabaseContext context)
    {
        var logs = context.LogEntries.OrderByDescending(l => l.Timestamp).ToList();
        Console.WriteLine("\nSystem Logs:");
        foreach (var log in logs)
        {
            Console.WriteLine($"[{log.Timestamp}] [{log.Level}] {log.Message} (by {log.Username})");
        }
    }
}

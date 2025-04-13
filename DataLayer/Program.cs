using DataLayer.Database;
using DataLayer.Logger;
using DataLayer.Model;
using Welcome.Others;

class Program
{
    static void Main(string[] args)
    {
        using var context = new DatabaseContext();
        context.Database.EnsureCreated();
        
        var logger = new Logger(context);
        
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
    
    static void ListAllUsers(DatabaseContext context, Logger logger)
    {
        var users = context.Users.ToList();
        Console.WriteLine("\nAll Users:");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Role: {user.Role}");
        }
        logger.LogAction("List Users", "Retrieved all users", "System");
    }
    
    static void AddNewUser(DatabaseContext context, Logger logger)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        
        var newUser = new DatabaseUser
        {
            Name = username,
            Password = password,
            Role = UserRole.STUDENT,
            Expires = DateTime.Now.AddYears(1)
        };
        
        context.Users.Add(newUser);
        context.SaveChanges();
        
        logger.LogAction("Add User", $"Added new user: {username}", "System");
        Console.WriteLine("User added successfully!");
    }
    
    static void DeleteUser(DatabaseContext context, Logger logger)
    {
        Console.Write("Enter username to delete: ");
        var username = Console.ReadLine();
        
        var user = context.Users.FirstOrDefault(u => u.Name == username);
        if (user != null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            logger.LogAction("Delete User", $"Deleted user: {username}", "System");
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine("User not found!");
        }
    }
    
    static void ListAllLogs(DatabaseContext context)
    {
        var logs = context.LogEntries.OrderByDescending(l => l.Timestamp).ToList();
        Console.WriteLine("\nAll Logs:");
        foreach (var log in logs)
        {
            Console.WriteLine($"ID: {log.Id}, Action: {log.Action}, Details: {log.Details}, Username: {log.Username}, Timestamp: {log.Timestamp}");
        }
    }
}

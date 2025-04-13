using DataLayer.Database;
using DataLayer.Model;
using Welcome.Others;
using Microsoft.EntityFrameworkCore;
using DataLayer.Repositories;
using DataLayer.Services;

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
        var disciplineRepository = new DisciplineRepository(context);
        var disciplineService = new DisciplineService(disciplineRepository);
        
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. User Management");
            Console.WriteLine("2. Discipline Management");
            Console.WriteLine("3. View Logs");
            Console.WriteLine("4. Exit");
            Console.Write("\nEnter your choice (1-4): ");
            
            var mainChoice = Console.ReadLine();
            
            switch (mainChoice)
            {
                case "1":
                    UserManagementMenu(context, logger);
                    break;
                case "2":
                    DisciplineManagementMenu(disciplineService, logger);
                    break;
                case "3":
                    ListAllLogs(context);
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    
    static void UserManagementMenu(DatabaseContext context, DataLayer.Logger logger)
    {
        while (true)
        {
            Console.WriteLine("\nUser Management Menu:");
            Console.WriteLine("1. List all users");
            Console.WriteLine("2. Add new user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. Back to main menu");
            Console.Write("\nEnter your choice (1-4): ");
            
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
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    
    static void DisciplineManagementMenu(IDisciplineService disciplineService, DataLayer.Logger logger)
    {
        while (true)
        {
            Console.WriteLine("\nDiscipline Management Menu:");
            Console.WriteLine("1. List all disciplines");
            Console.WriteLine("2. Add new discipline");
            Console.WriteLine("3. Edit discipline");
            Console.WriteLine("4. Delete discipline");
            Console.WriteLine("5. View discipline statistics");
            Console.WriteLine("6. Back to main menu");
            Console.Write("\nEnter your choice (1-6): ");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ListAllDisciplines(disciplineService, logger);
                    break;
                case "2":
                    AddNewDiscipline(disciplineService, logger);
                    break;
                case "3":
                    EditDiscipline(disciplineService, logger);
                    break;
                case "4":
                    DeleteDiscipline(disciplineService, logger);
                    break;
                case "5":
                    ViewDisciplineStatistics(disciplineService, logger);
                    break;
                case "6":
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
    
    static async void ListAllDisciplines(IDisciplineService disciplineService, DataLayer.Logger logger)
    {
        try
        {
            var disciplines = await disciplineService.GetAllDisciplinesAsync();
            Console.WriteLine("\nAll Disciplines:");
            foreach (var discipline in disciplines)
            {
                Console.WriteLine($"ID: {discipline.Id}, Name: {discipline.Name}, Year: {discipline.Year}, Semester: {discipline.Semester}, Credits: {discipline.Credits}, Lecturer: {discipline.Lecturer}");
            }
            logger.LogInfo("Retrieved all disciplines");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing disciplines: {ex.Message}");
            logger.LogError($"Error listing disciplines: {ex.Message}");
        }
    }
    
    static async void AddNewDiscipline(IDisciplineService disciplineService, DataLayer.Logger logger)
    {
        try
        {
            Console.Write("Enter discipline name: ");
            var name = Console.ReadLine();
            
            Console.Write("Enter description: ");
            var description = Console.ReadLine();
            
            Console.Write("Enter year (1-4): ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year < 1 || year > 4)
            {
                Console.WriteLine("Invalid year. Must be between 1 and 4.");
                return;
            }
            
            Console.Write("Enter semester (1-2): ");
            if (!int.TryParse(Console.ReadLine(), out int semester) || semester < 1 || semester > 2)
            {
                Console.WriteLine("Invalid semester. Must be 1 or 2.");
                return;
            }
            
            Console.Write("Enter credits: ");
            if (!int.TryParse(Console.ReadLine(), out int credits))
            {
                Console.WriteLine("Invalid credits value.");
                return;
            }
            
            Console.Write("Enter lecturer: ");
            var lecturer = Console.ReadLine();
            
            var newDiscipline = new Discipline
            {
                Name = name,
                Description = description,
                Year = year,
                Semester = semester,
                Credits = credits,
                Lecturer = lecturer,
                MaxStudents = 30,
                CreatedAt = DateTime.Now
            };
            
            await disciplineService.CreateDisciplineAsync(newDiscipline);
            logger.LogInfo($"Added new discipline: {name}");
            Console.WriteLine("Discipline added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding discipline: {ex.Message}");
            logger.LogError($"Error adding discipline: {ex.Message}");
        }
    }
    
    static async void EditDiscipline(IDisciplineService disciplineService, DataLayer.Logger logger)
    {
        try
        {
            Console.Write("Enter discipline ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }
            
            var discipline = await disciplineService.GetDisciplineByIdAsync(id);
            if (discipline == null)
            {
                Console.WriteLine("Discipline not found!");
                return;
            }
            
            Console.WriteLine($"Editing discipline: {discipline.Name}");
            
            Console.Write($"Enter new name [{discipline.Name}]: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                discipline.Name = name;
            
            Console.Write($"Enter new description [{discipline.Description}]: ");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
                discipline.Description = description;
            
            Console.Write($"Enter new year (1-4) [{discipline.Year}]: ");
            var yearInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(yearInput) && int.TryParse(yearInput, out int year) && year >= 1 && year <= 4)
                discipline.Year = year;
            
            Console.Write($"Enter new semester (1-2) [{discipline.Semester}]: ");
            var semesterInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(semesterInput) && int.TryParse(semesterInput, out int semester) && semester >= 1 && semester <= 2)
                discipline.Semester = semester;
            
            Console.Write($"Enter new credits [{discipline.Credits}]: ");
            var creditsInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(creditsInput) && int.TryParse(creditsInput, out int credits))
                discipline.Credits = credits;
            
            Console.Write($"Enter new lecturer [{discipline.Lecturer}]: ");
            var lecturer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lecturer))
                discipline.Lecturer = lecturer;
            
            await disciplineService.UpdateDisciplineAsync(discipline);
            logger.LogInfo($"Updated discipline: {discipline.Name}");
            Console.WriteLine("Discipline updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error editing discipline: {ex.Message}");
            logger.LogError($"Error editing discipline: {ex.Message}");
        }
    }
    
    static async void DeleteDiscipline(IDisciplineService disciplineService, DataLayer.Logger logger)
    {
        try
        {
            Console.Write("Enter discipline ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }
            
            var discipline = await disciplineService.GetDisciplineByIdAsync(id);
            if (discipline == null)
            {
                Console.WriteLine("Discipline not found!");
                return;
            }
            
            Console.Write($"Are you sure you want to delete {discipline.Name}? (y/n): ");
            var confirm = Console.ReadLine()?.ToLower();
            
            if (confirm == "y")
            {
                await disciplineService.DeleteDisciplineAsync(id);
                logger.LogInfo($"Deleted discipline: {discipline.Name}");
                Console.WriteLine("Discipline deleted successfully!");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting discipline: {ex.Message}");
            logger.LogError($"Error deleting discipline: {ex.Message}");
        }
    }
    
    static async void ViewDisciplineStatistics(IDisciplineService disciplineService, DataLayer.Logger logger)
    {
        try
        {
            var totalDisciplines = await disciplineService.GetTotalDisciplinesCountAsync();
            var yearStats = await disciplineService.GetDisciplineStatisticsByYearAsync();
            var semesterStats = await disciplineService.GetDisciplineStatisticsBySemesterAsync();
            
            Console.WriteLine("\nDiscipline Statistics:");
            Console.WriteLine($"Total Disciplines: {totalDisciplines}");
            
            Console.WriteLine("\nDisciplines per Year:");
            foreach (var stat in yearStats.OrderBy(s => s.Key))
            {
                Console.WriteLine($"Year {stat.Key}: {stat.Value} disciplines");
            }
            
            Console.WriteLine("\nDisciplines per Semester:");
            foreach (var stat in semesterStats.OrderBy(s => s.Key))
            {
                Console.WriteLine($"Semester {stat.Key}: {stat.Value} disciplines");
            }
            
            logger.LogInfo("Viewed discipline statistics");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error viewing discipline statistics: {ex.Message}");
            logger.LogError($"Error viewing discipline statistics: {ex.Message}");
        }
    }
}

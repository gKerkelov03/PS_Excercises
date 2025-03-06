using DataLayer.Database;
using DataLayer.Model;
using Welcome.Others;

using var context = new DatabaseContext();
context.Database.EnsureCreated();
context.Add<DatabaseUser>(new DatabaseUser()
{
      Name = "Angel",
      Password = "1234",
      Role = UserRole.STUDENT,
      Expires = DateTime.Now
});

context.SaveChanges();

Console.Write("Enter username: ");
var searchedUsername = Console.ReadLine();

Console.Write("Enter password: ");
var searchedPassword = Console.ReadLine();
    

var userExists = context.Users.Any(u => u.Name == searchedUsername && u.Password == searchedPassword);
if (userExists)
{
      Console.WriteLine($"User exists");
}
else
{
      Console.WriteLine($"User doesn't exist");
}

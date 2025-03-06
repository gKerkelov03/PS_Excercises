
using Welcome.Model;
using Welcome.Others;
using Welcome.View;
using Welcome.ViewModel;
using WelcomeExtended.Data;
using WelcomeExtended.Helpers;
using WelcomeExtended.Others;

try
{
    //From Excercise 2
    // var user = new User();
    // user.Name = "Gosho";
    // user.Password = "Gosho123";
    // user.Role = UserRole.STUDENT;
    //
    // var viewModel = new UserViewModel(user);
    // var view = new UserView(viewModel);
    // view.Display();
    // view.DisplayError();
    
    //From Excercise 3
    UserData userData = new UserData();
    
    var studentUser = new User();
    studentUser.Name = "gosho";
    studentUser.Password = "gosho123";
    studentUser.Role = UserRole.STUDENT;
    
    
    var student2 = new User();
    student2.Name = "Student2";
    student2.Password = "123";
    student2.Role = UserRole.STUDENT;
    userData.AddUser(student2);
    
    var teacher = new User();
    teacher.Name = "Teacher";
    teacher.Password = "1234";
    teacher.Role = UserRole.PROFESSOR;
    userData.AddUser(teacher);
    
    var admin = new User();
    admin.Name = "Admin";
    admin.Password = "12345";
    admin.Role = UserRole.ADMIN;
    userData.AddUser(admin);

    Console.Write("Enter username: ");
    var searchedName = Console.ReadLine();
    
    Console.Write("Enter password: ");
    var searchedPassword = Console.ReadLine();
    
    var userExists =     userData.ValidateCredentials(searchedName, searchedPassword);
    if (userExists)
    {
        var user = userData.GetUser(searchedName, searchedPassword);
        Console.WriteLine(user.ToStringHelper()); 
    }
    else
    {
        throw new Exception("User not found");
    }
}
catch (Exception e)
{
    var log = new ActionOnError(WelcomeExtended.Others.Delegate.Log);
    log(e.Message);
}
finally
{
    Console.WriteLine("Executed in any case!");
}


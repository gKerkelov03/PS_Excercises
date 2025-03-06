using Welcome.Model;
using WelcomeExtended.Data;

namespace WelcomeExtended.Helpers;

public static class UserDataHelper
{
    public static bool ValidateCredentials(this UserData userData, string name, string password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            throw new Exception("User credentials cannot be empty");
        }

        return userData.ValidateUser(name, password);
    }

    public static User GetUser(this UserData userData, string name, string password)
    {
       return userData.GetUser(name, password); 
    }
}
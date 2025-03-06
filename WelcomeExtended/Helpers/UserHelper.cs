using Welcome.Model;

namespace WelcomeExtended.Helpers;

public static class UserHelper
{
    public static string ToStringHelper(this User user)
    {
       return  $"{user.Name}: {user.Role}";
    }
}
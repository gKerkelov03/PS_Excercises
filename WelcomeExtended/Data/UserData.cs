using Welcome.Model;
using Welcome.Others;

namespace WelcomeExtended.Data;

public class UserData
{
    private List<User> _users = new List<User>();
    private int _nextId;

    public void AddUser(User user)
    {
        user.Id = ++_nextId;
        _users.Add(user);
    }
    
    public void DeleteUser(User user)
    {
        _users = _users.Where(u => u.Id != user.Id).ToList();
    }

    public bool ValidateUser(string username, string password )
    {
        foreach (var user in _users)
        {
            if (user.Name == user.Name && user.Password == password)
            {
               return true; 
            }
        } 
        
        return false;
    }

    public bool ValidateUserLambda(string username, string password)
    {
        return _users.Where(u => u.Name == username && u.Password == password).Any();
    }

    public bool ValidateUserLinq(string username, string password)
    {
        var result = from user in _users
            where user.Name == username && user.Password == password
            select user.Id;
        
        return result != null;
    }

    public User GetUser(string username, string password)
    {
        return _users.FirstOrDefault(u => u.Name == username && u.Password == password);
    }

    public void SetActive(string username, DateTime date)
    {
        var user = _users.FirstOrDefault(u => u.Name == username);
        
        if (user != null)
        {
            user.Expires = date; 
        }

        throw new Exception("No such user");
    }

    public void AssignUserRole(string username, UserRole role)
    {
        var user = _users.FirstOrDefault(u => u.Name == username);
        
        if (user != null)
        {
            user.Role = role; 
        }

        throw new Exception("No such user");
    }
}
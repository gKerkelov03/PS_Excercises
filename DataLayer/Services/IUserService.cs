using System.Threading.Tasks;
using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Services
{
    public interface IUserService
    {
        Task<DatabaseUser> GetUserByIdAsync(int id);
        Task<DatabaseUser> GetUserByNameAsync(string name);
        Task<IEnumerable<DatabaseUser>> GetAllUsersAsync();
        Task AddUserAsync(DatabaseUser user);
        Task UpdateUserAsync(DatabaseUser user);
        Task DeleteUserAsync(DatabaseUser user);
        Task<bool> ValidateUserAsync(string name, string password);
    }
} 
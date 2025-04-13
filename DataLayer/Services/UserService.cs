using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<DatabaseUser> _userRepository;

        public UserService(IRepository<DatabaseUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DatabaseUser> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<DatabaseUser> GetUserByNameAsync(string name)
        {
            var users = await _userRepository.FindAsync(u => u.Username == name);
            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<DatabaseUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(DatabaseUser user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(DatabaseUser user)
        {
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(DatabaseUser user)
        {
            await _userRepository.RemoveAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> ValidateUserAsync(string name, string password)
        {
            var user = await GetUserByNameAsync(name);
            return user != null && user.Password == password;
        }
    }
} 
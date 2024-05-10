using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Services.Interfaces;
using ECommerceApp.Model;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            
        }

        public async Task<List<Users>> GetUsers()
        {
            
            var users = await _userRepository.GetUsers();
            Log.Information("Retrieved {Count} users from the repository", users.Count);
            return users;

        }

        
        public async Task<Users> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAndPassword(email, password);
            if (user != null)
            {
                Log.Information("Retrieved user with email {Email} from the repository", email);
            }
            else
            {
                Log.Information("User with email {Email} not found in the repository", email);
            }
            return user;
        }
        public async Task<int> AddUser(Users user)
        {
          
            var result = await _userRepository.AddUser(user);
            Log.Information("Added user with ID {UserId}", result);
            return result;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var result= await _userRepository.DeleteUser(userId);
            Log.Information("Deleetd user with ID {UserId}", result);
            return result;

        }
    }
}

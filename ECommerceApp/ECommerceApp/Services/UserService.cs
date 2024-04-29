using ECommerceApp.Model;
using ECommerceApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<Users>> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public Task<Users> GetUserByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetUserByEmailAndPassword(email, password);
        }

        public Task<int> AddUser(Users user)
        {
            return _userRepository.AddUser(user);
        }

        public Task<bool> DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }
    }
}

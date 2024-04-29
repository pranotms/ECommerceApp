
using ECommerceApp.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApp.Repositories
{
    public interface IUserRepository
    {
        Task<List<Users>> GetUsers();
        Task<Users> GetUserByEmailAndPassword(string email, string password);
        Task<int> AddUser(Users user);
        Task<bool> DeleteUser(int userId);
    }
}

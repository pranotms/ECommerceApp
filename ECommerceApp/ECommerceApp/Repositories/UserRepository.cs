using ECommerceApp.Model;
using ECommerceApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserByEmailAndPassword(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<int> AddUser(Users user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

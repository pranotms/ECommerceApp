using ECommerceApp.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Repositories;
using BCrypt.Net;
namespace ECommerceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));        
        }
        public async Task<List<Users>> GetUsers()
        {
            using IDbConnection connection = _context.CreateConnection();

            var users = await connection.QueryAsync<Users>("sp_GetUsers", commandType: CommandType.StoredProcedure);
            return users.AsList();
        }
        public async Task<Users> GetUserByEmailAndPassword(string email, string password)
        {
            using IDbConnection connection = _context.CreateConnection();

            var user = await connection.QueryFirstOrDefaultAsync<Users>(
                "GetUserByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure);


            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<int> AddUser(Users user)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            using IDbConnection connection = _context.CreateConnection();
            var rowAffected = await connection.ExecuteAsync(
                "InsertUserProcedure",
                new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = hashedPassword
                },
                commandType: CommandType.StoredProcedure);

            return rowAffected;

        }
        public async Task<bool> DeleteUser(int userId)
        {
            using IDbConnection connection = _context.CreateConnection();

            var rowAffected = await connection.ExecuteAsync("DeleteUserById", new { UserId = userId }, commandType: CommandType.StoredProcedure);
            return rowAffected > 0;
        }
    }
}


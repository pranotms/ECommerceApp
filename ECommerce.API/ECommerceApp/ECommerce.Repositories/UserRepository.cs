

using ECommerceApp.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ECommerceAPI.ECommerce.Repositories.NewFolder;
using ECommerceAPI.ECommerce.Repositories;

namespace ECommerceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DapperContext context, ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Users>> GetUsers()
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                var users = (await connection.QueryAsync<Users>("SELECT ID,FirstName,LastName,Email,Password FROM Users")).AsList();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users");
                throw; 
            }
        }

        public async Task<Users> GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                var user = await connection.QueryFirstOrDefaultAsync<Users>("SELECT ID,FirstName,LastName,Email,Password FROM Users WHERE Email = @Email AND Password = @Password",
                    new { Email = email, Password = password });
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user by email and password");
                throw; 
            }
        }

        public async Task<int> AddUser(Users user)
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                var rowAffected = await connection.ExecuteAsync("INSERT INTO Users (FirstName,LastName,Email,Password) VALUES (@FirstName,@LastName,@Email,@Password)", user);
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user");
                throw; 
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                using IDbConnection connection = _context.CreateConnection();
                var rowAffected = await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @UserId", new { UserId = userId });
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user");
                throw; 
            }
        }
    }
}

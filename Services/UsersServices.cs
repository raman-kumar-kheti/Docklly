using Docklly.Database;
using Docklly.Models;
using Microsoft.EntityFrameworkCore;
namespace Docklly.Services
{
    /// <summary>
    /// Service for managing user operations
    /// </summary>
    public interface IUsersService
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(Guid id);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<Users> CreateUserAsync(Users user);
        Task<Users?> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> ActivateUserAsync(Guid id);
    }

    public class UsersServices : IUsersService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UsersServices> _logger;

        public UsersServices(AppDbContext dbContext, ILogger<UsersServices> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all users");
                return await _context.Users
                    .Where(u => u.IsActive)
                    .OrderByDescending(u => u.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                throw;
            }
        }

        public async Task<Users?> GetUserByIdAsync(Guid id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user by ID: {UserId}", id);
                throw;
            }
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user by email: {Email}", email);
                throw;
            }
        }

        public async Task<Users> CreateUserAsync(Users user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                user.IsActive = true;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User created: {UserId}", user.Id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        public async Task<Users?> UpdateUserAsync(Users user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (existingUser == null)
                    return null;

                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                existingUser.Department = user.Department;
                existingUser.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User updated: {UserId}", user.Id);
                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {UserId}", user.Id);
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return false;

                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User deactivated: {UserId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user: {UserId}", id);
                throw;
            }
        }

        public async Task<bool> ActivateUserAsync(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return false;

                user.IsActive = true;
                user.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User activated: {UserId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating user: {UserId}", id);
                throw;
            }
        }
    }
}

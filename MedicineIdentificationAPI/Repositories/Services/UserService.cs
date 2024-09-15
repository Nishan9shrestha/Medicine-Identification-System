using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class UserService : IUserRepository
    {
        private readonly MedicineDbContext _dbContext;

        public UserService(MedicineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            // Ensure the user does not have a set UserId
            user.UserId = Guid.NewGuid(); // Assign a new unique ID

            // Hash the user's password before saving
            user.PasswordHash = HashPassword(user.PasswordHash);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user is null)
                return null;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.UserId);
            if (existingUser is null)
                return null;

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;

            // If the password is being updated, hash it before saving
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                existingUser.PasswordHash = HashPassword(user.PasswordHash);
            }

            existingUser.Role = user.Role;
            existingUser.UpdatedAt = DateTime.UtcNow; // Update the UpdatedAt field

            await _dbContext.SaveChangesAsync();
            return existingUser;
        }

        // Example method to hash passwords - implement this according to your requirements
        private string HashPassword(string password)
        {
            // Use a secure hashing algorithm, e.g., bcrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

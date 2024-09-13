using MedicineIdentificationAPI.Models;
namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid userId); // Read a user by ID
        Task<IEnumerable<User>> GetAllUsersAsync(); // Get all users
        Task<List<User>> AddUserAsync(User user); // Create a new user
        Task<List<User>> UpdateUserAsync(User user); // Update user info
        Task DeleteUserAsync(Guid userId); // Delete a user
        Task<User> GetUserByEmailAsync(string email); // Retrieve user by email (for authentication)
    }
}
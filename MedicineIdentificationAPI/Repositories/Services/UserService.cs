using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class UserService : IUserRepository
    {
        private readonly MedicineDbContext _dbContext;

        public UserService(MedicineDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ActionResult<User>> AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return await  _dbContext.Users.ToListAsync();
        }

        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

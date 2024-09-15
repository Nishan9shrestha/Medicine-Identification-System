using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("No such data exists in the Users database.");

            return Ok(user);
        }

        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<User>> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound("Email does not exist in the database.");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                return Conflict("User already exists with this email."); // Or handle as needed

            var newUser = await _userRepository.AddUserAsync(user);
            return Ok(newUser);
        }



        [HttpPut("{userId:guid}")]
        public async Task<ActionResult<User>> UpdateUserAsync(Guid userId, [FromBody] User user)
        {
            if (userId != user.UserId)
                return BadRequest("User ID mismatch.");

            var updatedUser = await _userRepository.UpdateUserAsync(user);
            if (updatedUser == null)
                return NotFound("User does not exist.");

            return Ok(updatedUser);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.DeleteUserAsync(userId);
            if (user == null)
                return NotFound("User does not exist.");

            return Ok("Data has been Deleted From the Database"); // Use NoContent to indicate successful deletion
        }
    }
}

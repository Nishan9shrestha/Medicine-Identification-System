using Microsoft.AspNetCore.Mvc;
using MedicineIdentificationAPI.Models; // Ensure this namespace matches where your DbContext is defined
using System.Linq;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MedicineDbContext _context;

        public TestController(MedicineDbContext context)
        {
            _context = context;
        }

        // GET: api/test/test-connection
        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                // Perform a simple query to test the connection
                var medicineCount = _context.Medicines.Count();

                return Ok(new
                {
                    Message = "Database connection is working.",
                    MedicineCount = medicineCount
                });
            }
            catch (Exception ex)
            {
                // Return an error message if there is an issue with the connection
                return StatusCode(500, new { Message = "Error connecting to the database.", Error = ex.Message });
            }
        }
    }
}

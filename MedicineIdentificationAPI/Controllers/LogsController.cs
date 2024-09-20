using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogRepository _logRepository;

        public LogsController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet("byLogId/{logId:guid}")]
        public async Task<ActionResult<Log>> GetLogByIdAsync(Guid logId)
        {
            var log = await _logRepository.GetLogByIdAsync(logId);
            if (log is null)
                return NotFound("Log with the given ID does not exist in the database.");

            return Ok(log);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetAllLogsAsync()
        {
            var logs = await _logRepository.GetAllLogsAsync();
            return Ok(logs);
        }

        [HttpPost]
        public async Task<ActionResult> AddLogAsync(Log log)
        {
            await _logRepository.AddLogAsync(log);
            return Ok(new { message = "Log has been added successfully." });
        }

        [HttpDelete("byLogId/{logId:guid}")]
        public async Task<ActionResult> DeleteLogAsync(Guid logId)
        {
            var existingLog = await _logRepository.GetLogByIdAsync(logId);
            if (existingLog is null)
                return NotFound("Log with the given ID does not exist in the database.");

            await _logRepository.DeleteLogAsync(logId);
            return Ok(new { message = "Log has been deleted successfully." });
        }

        [HttpGet("byUserId/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogByUserId(Guid userId)
        {
            try
            {
                var UserId = await _logRepository.GetLogByUserId(userId);
                if (UserId == null) return NotFound();

                return Ok(UserId);

            }
            catch (ArgumentException ex) { 
            return BadRequest(ex.Message);
            }

        }
    }
}

using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosageScheduleController : ControllerBase
    {
        private readonly IDosageScheduleRepository _dosageSchedule;

        public DosageScheduleController(IDosageScheduleRepository dosageSchedule)
        {
            _dosageSchedule = dosageSchedule;
        }

        [HttpGet("byScheduleId/{scheduleId:guid}")]
        public async Task<ActionResult<Models.DosageSchedule>> GetScheduleByIdAsync(Guid scheduleId)
        {
            var schedule = await _dosageSchedule.GetScheduleByIdAsync(scheduleId);
            if (schedule == null)
            {
                return NotFound("Schedule with the given Id does not exist in the database.");
            }

            return Ok(schedule);
        }

        [HttpGet("byMedicineId/{medicineId:guid}")]
        public async Task<ActionResult<IEnumerable<Models.DosageSchedule>>> GetSchedulesByMedicineIdAsync(Guid medicineId)
        {
            try
            {
                var schedules = await _dosageSchedule.GetSchedulesByMedicineIdAsync(medicineId);
                return Ok(schedules);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddScheduleAsync(DosageSchedule schedule)
        {
            await _dosageSchedule.AddScheduleAsync(schedule);
            return Ok(new { message = "Schedule has been added successfully to the database." });
        }

        [HttpPut("byScheduleId/{scheduleId:guid}")]
        public async Task<ActionResult> UpdateScheduleAsync(Guid scheduleId, [FromBody] DosageSchedule schedule)
        {
            if (scheduleId == Guid.Empty)
                return BadRequest("Invalid Schedule Id.");

            if (schedule.ScheduleId != scheduleId)
                return BadRequest("The ScheduleId in the URL does not match the ScheduleId in the body.");

            try
            {
                await _dosageSchedule.UpdateScheduleAsync(schedule);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("byScheduleId/{scheduleId:guid}")]
        public async Task<ActionResult> DeleteScheduleAsync(Guid scheduleId)
        {
            if (scheduleId == Guid.Empty)
                return BadRequest("Invalid Schedule Id.");

            var schedule = await _dosageSchedule.GetScheduleByIdAsync(scheduleId);
            if (schedule == null)
                return NotFound("Schedule with the given Id does not exist in the database.");

            await _dosageSchedule.DeleteScheduleAsync(scheduleId);
            return Ok(new { message = "Schedule has been removed successfully from the database." });
        }
    }
}

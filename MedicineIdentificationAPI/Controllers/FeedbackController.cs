using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet("FeedbackId/{feedbackId:guid}")]
        public async Task<ActionResult<Feedback>> GetFeedbackByIdAsync(Guid feedbackId)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(feedbackId);
            if (feedback is null)
                return NotFound("Feedback does not exist.");

            return Ok(feedback);
        }

        [HttpGet("bypredictionId/{predictionId:guid}")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksByPredictionIdAsync(Guid predictionId)
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetFeedbacksByPredictionIdAsync(predictionId);
                if (!feedbacks.Any())
                {
                    return NotFound("No feedback exists for this prediction.");
                }
                return Ok(feedbacks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddFeedbackAsync(Feedback feedback)
        {
            if (feedback == null)
                return BadRequest("Invalid feedback data.");

            await _feedbackRepository.AddFeedbackAsync(feedback);
            return Ok(new { message = "Feedback has been added successfully." });
        }

        [HttpDelete("byFeedbackId/{feedbackId:guid}")]
        public async Task<IActionResult> DeleteFeedbackAsync(Guid feedbackId)
        {
            if (feedbackId == Guid.Empty)
                return BadRequest("Invalid ID.");

            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(feedbackId);
            if (feedback == null)
                return NotFound("Feedback does not exist for this ID.");

            await _feedbackRepository.DeleteFeedbackAsync(feedbackId);
            return Ok(new { message = "Feedback deleted successfully." });
        }
    }
}

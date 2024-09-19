using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionRepository _predictionRepository;

        public PredictionController(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        [HttpGet("byPredictionId/{predictionId:guid}")]
        public async Task<ActionResult<Prediction>> GetPredictionByIdAsync(Guid predictionId)
        {
            var predict = await _predictionRepository.GetPredictionByIdAsync(predictionId);
            if (predict is null)
                return NotFound("No such value Exists");

            return Ok(predict);
        }

        [HttpGet("byUserId/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<Prediction>>> GetPredictionsByUserIdAsync(Guid userId)
        {
            try
            {
                var predictions = await _predictionRepository.GetPredictionsByUserIdAsync(userId);

                if (!predictions.Any())
                    return NotFound("No predictions found for this user");

                return Ok(predictions);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Return 404 with the user not found message
            }
        }



        [HttpPost]
        public async Task<ActionResult> AddPredictionAsync(Prediction prediction)
        {
            await _predictionRepository.AddPredictionAsync(prediction);
            return Ok(new { message = "Prediction has been added Successfully" });
        }


        [HttpPut("FromPredictionId/{predictionId:guid}")]
        public async Task<ActionResult> UpdatePredictionAsync(Guid predictionId, [FromBody] Prediction prediction)
        {
            if (prediction == null)
                return BadRequest("Invalid prediction data");

            var existingPrediction = await _predictionRepository.GetPredictionByIdAsync(predictionId);
            if (existingPrediction == null)
                return NotFound("Prediction does not exist");

            await _predictionRepository.UpdatePredictionAsync(prediction);

            return Ok(new { message = "Prediction updated successfully" });
        }


        [HttpDelete("byPredictionId/{predictionId:guid}")]
        public async Task<IActionResult> DeletePredictionAsync(Guid predictionId)
        {
            if (predictionId == Guid.Empty)
                return BadRequest("Invalid prediction ID");

            var prediction = await _predictionRepository.GetPredictionByIdAsync(predictionId);
            if (prediction == null)
                return NotFound("Prediction does not exist");

            await _predictionRepository.DeletePredictionAsync(predictionId);

            return Ok(new { message = "Prediction deleted successfully" });
        }

    }
}

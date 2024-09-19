using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineImageController : ControllerBase
    {
        private readonly IMedicineImageRepository _medicineImageRepository;

        public MedicineImageController(IMedicineImageRepository medicineImageRepository)
        {
            _medicineImageRepository = medicineImageRepository;
        }

        [HttpGet("byimageId/{imageId:guid}")]
        public async Task<ActionResult<MedicineImage>> GetImageByIdAsync(Guid imageId)
        {
            var imageid = await _medicineImageRepository.GetImagesByMedicineIdAsync(imageId);
            if (imageid is null)
                return NotFound("The image doesnot exists");

            return Ok(imageid);
        }

        [HttpGet("bymedicineId/{medicineId:guid}")]
        public async Task<ActionResult<IEnumerable<MedicineImage>>> GetImagesByMedicineIdAsync(Guid medicineId)
        {
            var Medicineid = await _medicineImageRepository.GetImagesByMedicineIdAsync(medicineId);
            if (Medicineid is null)
                return NotFound("No image exists with that Medicine Id");

            return Ok(Medicineid);

        }

        [HttpPost]
        public async Task<ActionResult> AddImageAsync(MedicineImage image, IFormFile imageFile)
        {
            await _medicineImageRepository.AddImageAsync(image,imageFile);

            return Ok(new {message = "New Image has been added Successfully"});
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<ActionResult> DeleteImageAsync(Guid imageId)
        {
            if (imageId == Guid.Empty) // Use Guid.Empty to check for an uninitialized GUID
            {
                return BadRequest(new { message = "Invalid image ID" });
            }

            await _medicineImageRepository.DeleteImageAsync(imageId);

            return Ok(new { message = "Image deleted successfully" });
        }




    }
}

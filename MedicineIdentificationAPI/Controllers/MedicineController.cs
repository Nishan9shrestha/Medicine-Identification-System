using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicineIdentificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetAllMedicinesAsync()
        {
            var medicines = await _medicineRepository.GetAllMedicinesAsync();
            return Ok(medicines);
        }

        [HttpGet("{medicineId:guid}")]
        public async Task<ActionResult<Medicine>> GetMedicineByIdAsync(Guid medicineId)
        {
            var medicine = await _medicineRepository.GetMedicineByIdAsync(medicineId);
            if (medicine == null)
                return NotFound("No such medicine exists");

            return Ok(medicine);
        }

        [HttpGet("byname/{medicineName}")]
        public async Task<ActionResult<Medicine>> GetMedicineByNameAsync(string medicineName)
        {
            var medicine = await _medicineRepository.GetMedicineByNameAsync(medicineName);
            if (medicine == null)
                return NotFound("No medicine with that name exists");

            return Ok(medicine);
        }

        [HttpPost]
        public async Task<ActionResult<Medicine>> AddMedicineAsync([FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMedicine = await _medicineRepository.GetMedicineByNameAsync(medicine.Name);
            if (existingMedicine != null)
                return Conflict("Medicine already exists");

            var createdMedicine = await _medicineRepository.AddMedicineAsync(medicine);
            return createdMedicine;
        }

        [HttpPut("{medicineId:guid}")]
        public async Task<ActionResult<Medicine>> UpdateMedicineAsync(Guid medicineId, [FromBody] Medicine medicine)
        {
            if (medicineId != medicine.MedicineId)
                return BadRequest("Medicine ID mismatch");

            var updatedMedicine = await _medicineRepository.UpdateMedicineAsync(medicine);
            if (updatedMedicine == null)
                return NotFound("Medicine not found");

            return Ok(updatedMedicine);
        }

        [HttpDelete("{medicineId:guid}")]
        public async Task<IActionResult> DeleteMedicineAsync(Guid medicineId)
        {
            var isdeleted = await _medicineRepository.DeleteMedicineAsync(medicineId);
            if (isdeleted is null)
                return NotFound("Medicine not found");

            return Ok("Medicine data has been deleted Successfully");
        }
    }
}

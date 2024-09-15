using MedicineIdentificationAPI.Models;

namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface IMedicineRepository
    {//in this code Medicine object is passed as a paramater because to do encapsulation and to avoid defining multiple paramater to pass those values
        Task<Medicine> GetMedicineByIdAsync(Guid medicineId); // Get a medicine by ID
        Task<IEnumerable<Medicine>> GetAllMedicinesAsync(); // Get all medicines
        Task AddMedicineAsync(Medicine medicine); // Create a new medicine
        Task UpdateMedicineAsync(Medicine medicine); // Update medicine details
        Task DeleteMedicineAsync(Guid medicineId); // Delete a medicine

    }
}

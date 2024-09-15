using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class MedicineServices : IMedicineRepository
    {
        public Task AddMedicineAsync(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMedicineAsync(Guid medicineId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Medicine>> GetAllMedicinesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Medicine> GetMedicineByIdAsync(Guid medicineId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMedicineAsync(Medicine medicine)
        {
            throw new NotImplementedException();
        }
    }
}

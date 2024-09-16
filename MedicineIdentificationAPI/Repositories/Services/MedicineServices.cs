using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class MedicineServices : IMedicineRepository
    {
        private readonly MedicineDbContext _medicineDbContext;

        public MedicineServices(MedicineDbContext medicineDbContext)
        {
            _medicineDbContext = medicineDbContext;
        }


        public async Task<Medicine> AddMedicineAsync(Medicine medicine)
        {
            medicine.MedicineId = Guid.NewGuid();//this adds a new id each time the medicine is added

            _medicineDbContext.Medicines.Add(medicine);
             await _medicineDbContext.SaveChangesAsync();
            return  medicine;

        }

        public async Task<Medicine> DeleteMedicineAsync(Guid medicineId)
        {
            var mId=await _medicineDbContext.Medicines.FindAsync(medicineId);
            if (mId is null)
                return null;

            _medicineDbContext.Medicines.Remove(mId);
            await _medicineDbContext.SaveChangesAsync();
            return mId;


        }

        public async Task<IEnumerable<Medicine>> GetAllMedicinesAsync()
        {
            return await _medicineDbContext.Medicines.ToListAsync();
        }

        public async Task<Medicine> GetMedicineByIdAsync(Guid medicineId)
        {
            var Mid= await _medicineDbContext.Medicines.FindAsync(medicineId);
            if (Mid is null)
                return null;

            return Mid;

        }
        public async Task<Medicine> GetMedicineByNameAsync(string name)
        {
            return await _medicineDbContext.Medicines.FirstOrDefaultAsync(x => x.Name==name);
        }


        public async Task<Medicine> UpdateMedicineAsync(Medicine medicine)
        {
            var mId=await _medicineDbContext.Medicines.FindAsync(medicine.MedicineId);
            if (mId is null)
                return null;

            mId.Name = medicine.Name;
            mId.Description = medicine.Description;
            mId.UsageInstructions = medicine.UsageInstructions;
            mId.Dosage = medicine.Dosage;
            mId.Manufacturer = medicine.Manufacturer;
            mId.UpdatedAt = DateTime.UtcNow;

            _medicineDbContext.SaveChanges();
            return mId;

        }
    }
}

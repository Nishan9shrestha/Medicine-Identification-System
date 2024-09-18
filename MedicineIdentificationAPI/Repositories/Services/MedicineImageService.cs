using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class MedicineImageService : IMedicineImageRepository
    {
        private readonly MedicineDbContext _medicineImage;

        public MedicineImageService(MedicineDbContext medicineImage)
        {
            _medicineImage = medicineImage;
        }

        public Task AddImageAsync(MedicineImage image, IFormFile imageFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteImageAsync(Guid imageId)
        {
            throw new NotImplementedException();
        }

        public Task<MedicineImage> GetImageByIdAsync(Guid imageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MedicineImage>> GetImagesByMedicineIdAsync(Guid medicineId)
        {
            throw new NotImplementedException();
        }
    }
}

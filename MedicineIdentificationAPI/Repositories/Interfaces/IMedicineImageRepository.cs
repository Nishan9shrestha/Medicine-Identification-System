using MedicineIdentificationAPI.Models;

namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface IMedicineImageRepository
    {
        Task<MedicineImage> GetImageByIdAsync(Guid imageId); // Get a specific image

        Task<IEnumerable<MedicineImage>> GetImagesByMedicineIdAsync(Guid medicineId); // Get images for a specific medicine

        Task AddImageAsync(MedicineImage image, IFormFile imageFile); // Add a new image with file upload

        Task DeleteImageAsync(Guid imageId); // Delete an image and the corresponding file
    }
}

using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class MedicineImageService : IMedicineImageRepository
    {
        private readonly MedicineDbContext _medicineDbContext;

        public MedicineImageService(MedicineDbContext medicineDbContext)
        {
            _medicineDbContext = medicineDbContext;
        }

        public async Task<MedicineImage> GetImageByIdAsync(Guid imageId)
        {
            return await _medicineDbContext.MedicineImages.FindAsync(imageId);
        }

        public async Task<IEnumerable<MedicineImage>> GetImagesByMedicineIdAsync(Guid medicineId)
        {
            return await _medicineDbContext.MedicineImages
                .Where(mi => mi.MedicineId == medicineId)
                .ToListAsync();
        }

        public async Task AddImageAsync(MedicineImage image, IFormFile imageFile)
        {
            // Save image to directory
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Medicines");
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory); // Ensure the directory exists
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Set image URL in the database (relative path or just filename)
            image.ImageUrl = fileName;
            image.UploadedAt = DateTime.UtcNow;

            // Add the image record to the database
            _medicineDbContext.MedicineImages.Add(image);
            await _medicineDbContext.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(Guid imageId)
        {
            var image = await _medicineDbContext.MedicineImages.FindAsync(imageId);
            if (image == null) return;

            // Delete the image file from the directory
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Medicines", image.ImageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove the image from the database
            _medicineDbContext.MedicineImages.Remove(image);
            await _medicineDbContext.SaveChangesAsync();
        }
    }
}

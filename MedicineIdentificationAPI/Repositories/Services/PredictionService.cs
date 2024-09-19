using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class PredictionService : IPredictionRepository
    {
        private readonly MedicineDbContext _medicineDbContext;

        public PredictionService(MedicineDbContext medicineDbContext)
        {
            _medicineDbContext = medicineDbContext;
        }

        public async Task AddPredictionAsync(Prediction prediction)
        {
            _medicineDbContext.Predictions.Add(prediction);
            await _medicineDbContext.SaveChangesAsync();
        }

        public async Task DeletePredictionAsync(Guid predictionId)
        {
            var prediction = await _medicineDbContext.Predictions.FindAsync(predictionId);
            if (prediction != null)
            {
                _medicineDbContext.Predictions.Remove(prediction);
                await _medicineDbContext.SaveChangesAsync();
            }
        }

        public async Task<Prediction> GetPredictionByIdAsync(Guid predictionId)
        {
            var prediction = await _medicineDbContext.Predictions.FindAsync(predictionId);
            return prediction; // No need for a null check, just return the result (it will return null if not found).
        }

        public async Task<IEnumerable<Prediction>> GetPredictionsByUserIdAsync(Guid userId)
        {
            // Check if the user exists in the Users table
            var userExists = await _medicineDbContext.Users.AnyAsync(u => u.UserId == userId);

            if (!userExists)
            {
                // Return null or throw an exception if the user doesn't exist
                throw new ArgumentException("User does not exist.");
            }

            // If user exists, proceed to get their predictions
            var predictions = await _medicineDbContext.Predictions
                .Where(x => x.UserId == userId)
                .ToListAsync();

            // Return an empty list if no predictions found
            return predictions.Any() ? predictions : new List<Prediction>();
        }


        public async Task<Prediction> UpdatePredictionAsync(Prediction prediction)
        {
            var existingPrediction = await _medicineDbContext.Predictions
                .FirstOrDefaultAsync(p => p.PredictionId == prediction.PredictionId);

            if (existingPrediction == null)
            {
                throw new ArgumentException("Prediction not found");
            }

            // Update the properties of the existing prediction with the new values
            existingPrediction.UserId = prediction.UserId;
            existingPrediction.ImageId = prediction.ImageId;
            existingPrediction.PredictedMedicineId = prediction.PredictedMedicineId;
            existingPrediction.ConfidenceScore = prediction.ConfidenceScore;
            existingPrediction.PredictedAt = prediction.PredictedAt ?? existingPrediction.PredictedAt;
            existingPrediction.IsConfirmed = prediction.IsConfirmed ?? existingPrediction.IsConfirmed;

            // Save changes to the database
            await _medicineDbContext.SaveChangesAsync();
            return existingPrediction;
        }
    }
}

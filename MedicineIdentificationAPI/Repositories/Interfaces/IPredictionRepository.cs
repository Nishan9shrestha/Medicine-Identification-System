using MedicineIdentificationAPI.Models;

namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface IPredictionRepository
    {
        Task<Prediction> GetPredictionByIdAsync(Guid predictionId); // Get a specific prediction
        Task<IEnumerable<Prediction>> GetPredictionsByUserIdAsync(Guid userId); // Get all predictions for a user
        Task AddPredictionAsync(Prediction prediction); // Add a new prediction
        Task<Prediction> UpdatePredictionAsync(Prediction prediction); // Update a prediction
        Task DeletePredictionAsync(Guid predictionId); // Delete a prediction

    }
}

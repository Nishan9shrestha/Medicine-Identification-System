using MedicineIdentificationAPI.Models;

namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetFeedbackByIdAsync(Guid feedbackId); // Get feedback by ID
        Task<IEnumerable<Feedback>> GetFeedbacksByPredictionIdAsync(Guid predictionId); // Get feedback for a prediction
        Task AddFeedbackAsync(Feedback feedback); // Add new feedback
        Task DeleteFeedbackAsync(Guid feedbackId); // Delete feedback

    }
}

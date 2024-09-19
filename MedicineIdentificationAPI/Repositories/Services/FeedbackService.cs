using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore; // Needed for async queries

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class FeedbackService : IFeedbackRepository
    {
        private readonly MedicineDbContext _dbContext;

        public FeedbackService(MedicineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            _dbContext.Feedbacks.Add(feedback);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(Guid feedbackId)
        {
            var feedback = await _dbContext.Feedbacks.FindAsync(feedbackId);
            if (feedback is null) return;

            _dbContext.Feedbacks.Remove(feedback);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid feedbackId)
        {
            var feedback = await _dbContext.Feedbacks.FindAsync(feedbackId);
            if (feedback is null) return null;

            return feedback;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByPredictionIdAsync(Guid predictionId)
        {
            var preId = await _dbContext.Predictions.FindAsync(predictionId);

            if (preId is null)
            {
                throw new ArgumentException("Prediction Id doesnot exist.");
            }

            return await _dbContext.Feedbacks
                .Where(f => f.PredictionId == predictionId)
                .ToListAsync();
        }

    }
}

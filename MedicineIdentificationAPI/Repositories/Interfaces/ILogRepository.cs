using MedicineIdentificationAPI.Models;

namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<Log> GetLogByIdAsync(Guid logId); // Get a log entry
        Task<IEnumerable<Log>> GetAllLogsAsync(); // Get all logs
        Task AddLogAsync(Log log); // Add a new log entry
        Task<IEnumerable<Log>> GetLogByUserId(Guid userId);
        Task DeleteLogAsync(Guid logId); // Delete a log entry

    }
}

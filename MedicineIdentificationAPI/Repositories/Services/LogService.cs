using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Models;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class LogService : ILogRepository
    {
        private readonly MedicineDbContext _medicineDb;

        public LogService(MedicineDbContext medicineDb)
        {
            _medicineDb = medicineDb;
        }

        public async Task AddLogAsync(Log log)
        {
            await _medicineDb.Logs.AddAsync(log);
            await _medicineDb.SaveChangesAsync();
        }

        public async Task DeleteLogAsync(Guid logId)
        {
            var log = await _medicineDb.Logs.FindAsync(logId);
            if (log is null)
                return;

            _medicineDb.Logs.Remove(log);
            await _medicineDb.SaveChangesAsync();
        }

        public async Task<IEnumerable<Log>> GetAllLogsAsync()
        {
            return await _medicineDb.Logs.ToListAsync();
        }

        public async Task<Log> GetLogByIdAsync(Guid logId)
        {
            var log = await _medicineDb.Logs.FindAsync(logId);
            return log;
        }

        public async Task<IEnumerable<Log>> GetLogByUserId(Guid userId)
        {
            var UserId= await _medicineDb.Users.FindAsync(userId);
            if (UserId is null)
            {
                throw new ArgumentException("Uer Id doesnot Exists");
            }

            return await _medicineDb.Logs.Where(L => L.UserId == userId).ToListAsync();
        }
    }
}

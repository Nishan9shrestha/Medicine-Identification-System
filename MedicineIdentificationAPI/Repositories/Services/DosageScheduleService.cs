using MedicineIdentificationAPI.Data;
using MedicineIdentificationAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicineIdentificationAPI.Repositories.Services
{
    public class DosageScheduleService : IDosageScheduleRepository
    {
        private readonly MedicineDbContext _medicineDb;

        public DosageScheduleService(MedicineDbContext medicineDb)
        {
            _medicineDb = medicineDb;
        }

        public async Task AddScheduleAsync(Models.DosageSchedule schedule)
        {
            _medicineDb.DosageSchedules.Add(schedule);
            await _medicineDb.SaveChangesAsync();
        }

        public async Task DeleteScheduleAsync(Guid scheduleId)
        {
            var schedule = await _medicineDb.DosageSchedules.FindAsync(scheduleId);
            if (schedule is null) return;

            _medicineDb.DosageSchedules.Remove(schedule);
            await _medicineDb.SaveChangesAsync(); // Missing save changes here
        }

        public async Task<Models.DosageSchedule> GetScheduleByIdAsync(Guid scheduleId)
        {
            return await _medicineDb.DosageSchedules.FindAsync(scheduleId);
        }

        public async Task<IEnumerable<Models.DosageSchedule>> GetSchedulesByMedicineIdAsync(Guid medicineId)
        {
            var medicineExists = await _medicineDb.Medicines.AnyAsync(m => m.MedicineId == medicineId);
            if (!medicineExists)
            {
                throw new ArgumentException("Medicine with that id does not exist in the database.");
            }

            return await _medicineDb.DosageSchedules.Where(ds => ds.MedicineId == medicineId).ToListAsync();
        }

        public async Task UpdateScheduleAsync(Models.DosageSchedule schedule)
        {
            var existingSchedule = await _medicineDb.DosageSchedules.FirstOrDefaultAsync(x => x.ScheduleId == schedule.ScheduleId);
            if (existingSchedule is null)
            {
                throw new ArgumentException("Dosage schedule not found.");
            }

            existingSchedule.MedicineId = schedule.MedicineId;
            existingSchedule.Time = schedule.Time;
            existingSchedule.DosageAmount = schedule.DosageAmount;
            existingSchedule.Frequency = schedule.Frequency;
            existingSchedule.Notes = schedule.Notes;

            await _medicineDb.SaveChangesAsync();
        }
    }
}

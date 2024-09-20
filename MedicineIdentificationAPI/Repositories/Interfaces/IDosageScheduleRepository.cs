using MedicineIdentificationAPI.Models;

namespace MedicineIdentificationAPI.Repositories.Interfaces
{
    public interface IDosageScheduleRepository
    {
        Task<DosageSchedule> GetScheduleByIdAsync(Guid scheduleId); // Get a schedule by ID
        Task<IEnumerable<DosageSchedule>> GetSchedulesByMedicineIdAsync(Guid medicineId); // Get schedules for a medicine
        Task AddScheduleAsync(DosageSchedule schedule); // Add a new schedule
        Task UpdateScheduleAsync(DosageSchedule schedule); // Update a schedule
        Task DeleteScheduleAsync(Guid scheduleId); // Delete a schedule

    }
}

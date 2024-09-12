using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class DosageSchedule
{
    public Guid ScheduleId { get; set; }

    public Guid? MedicineId { get; set; }

    public TimeOnly Time { get; set; }

    public string DosageAmount { get; set; } = null!;

    public string? Frequency { get; set; }

    public string? Notes { get; set; }

    public virtual Medicine? Medicine { get; set; }
}

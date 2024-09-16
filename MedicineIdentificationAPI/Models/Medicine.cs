using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class Medicine
{
    public Guid MedicineId { get; set; }= Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? UsageInstructions { get; set; }

    public string? Dosage { get; set; }

    public string? Manufacturer { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DosageSchedule> DosageSchedules { get; set; } = new List<DosageSchedule>();

    public virtual ICollection<MedicineImage> MedicineImages { get; set; } = new List<MedicineImage>();

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}

using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class Prediction
{
    public Guid PredictionId { get; set; } = Guid.NewGuid();

    public Guid? UserId { get; set; }

    public Guid? ImageId { get; set; }

    public Guid? PredictedMedicineId { get; set; }

    public double ConfidenceScore { get; set; }

    public DateTime? PredictedAt { get; set; }

    public bool? IsConfirmed { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual MedicineImage? Image { get; set; }

    public virtual Medicine? PredictedMedicine { get; set; }

    public virtual User? User { get; set; }
}

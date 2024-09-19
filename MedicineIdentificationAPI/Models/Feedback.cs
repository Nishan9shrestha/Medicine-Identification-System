using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class Feedback
{
    public Guid FeedbackId { get; set; }= Guid.NewGuid();

    public Guid? PredictionId { get; set; }

    public Guid? UserId { get; set; }

    public bool IsCorrect { get; set; }

    public string? Comments { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual Prediction? Prediction { get; set; }

    public virtual User? User { get; set; }
}

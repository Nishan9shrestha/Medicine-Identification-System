using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<MedicineImage> MedicineImages { get; set; } = new List<MedicineImage>();

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}

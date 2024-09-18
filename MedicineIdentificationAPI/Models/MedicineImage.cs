using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class MedicineImage
{
    public Guid ImageId { get; set; } = new Guid();

    public Guid? MedicineId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public Guid? UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();

    public virtual User? UploadedByNavigation { get; set; }
}

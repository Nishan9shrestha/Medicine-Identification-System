using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicineIdentificationAPI.Models;

public partial class MedicineImage
{
    public Guid ImageId { get; set; } = Guid.NewGuid();

    [Required]
    public Guid? MedicineId { get; set; }

    [Required]
    [StringLength(266)]
    public string ImageUrl { get; set; } = null!;

    [StringLength(255)]
    public string? ThumbnailUrl { get; set; }

    public Guid? UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();

    public virtual User? UploadedByNavigation { get; set; }
}

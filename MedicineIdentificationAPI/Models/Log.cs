using System;
using System.Collections.Generic;

namespace MedicineIdentificationAPI.Models;

public partial class Log
{
    public Guid LogId { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? Details { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual User? User { get; set; }
}

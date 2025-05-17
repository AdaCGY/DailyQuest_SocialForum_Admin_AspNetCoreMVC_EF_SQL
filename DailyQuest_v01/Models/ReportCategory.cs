using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class ReportCategory
{
    public int ReportCategoryId { get; set; }

    public string ReportCategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}

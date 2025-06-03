using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int MemberId { get; set; }

    public int ReportCategoryId { get; set; }

    public int PostId { get; set; }

    public DateTime ReportedAt { get; set; }

    public string ReportContent { get; set; } = null!;

    public int? AdminId { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public string? AdminComment { get; set; }

    public string Status { get; set; } = null!;

    public virtual Admin? Admin { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual ReportCategory ReportCategory { get; set; } = null!;
}

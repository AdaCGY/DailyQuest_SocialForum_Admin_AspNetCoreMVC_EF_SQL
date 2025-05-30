using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Tool
{
    public int ToolId { get; set; }

    public string ToolName { get; set; } = null!;

    public string? ToolDescription { get; set; }

    public byte[]? ToolPhoto { get; set; }

    public int PointValue { get; set; }

    public int CurrentStock { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}

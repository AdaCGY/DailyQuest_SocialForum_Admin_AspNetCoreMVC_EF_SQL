using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class SubTask
{
    public int SubTaskId { get; set; }

    public int TaskId { get; set; }

    public string TaskContent { get; set; } = null!;

    public DateTime ExpectDate { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public int Points { get; set; }

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual Mission Task { get; set; } = null!;
}

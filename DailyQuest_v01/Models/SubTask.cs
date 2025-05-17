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

    public virtual Task Task { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

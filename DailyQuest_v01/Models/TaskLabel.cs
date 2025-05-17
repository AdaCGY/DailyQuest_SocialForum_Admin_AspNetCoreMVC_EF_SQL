using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class TaskLabel
{
    public int TaskLabelId { get; set; }

    public string TaskLabelName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class TaskResult
{
    public int TaskResultId { get; set; }

    public string TaskResultName { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<UserAndTaskR> UserAndTaskRs { get; set; } = new List<UserAndTaskR>();
}

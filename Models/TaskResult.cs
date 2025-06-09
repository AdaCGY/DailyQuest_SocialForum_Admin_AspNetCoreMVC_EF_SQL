using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class TaskResult
{
    public int TaskResultId { get; set; }

    public string TaskResultName { get; set; } = null!;

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual ICollection<UserAndTaskR> UserAndTaskRs { get; set; } = new List<UserAndTaskR>();
}

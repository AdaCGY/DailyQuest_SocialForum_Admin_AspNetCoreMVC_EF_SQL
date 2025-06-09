using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class TaskType
{
    public int TaskTypeId { get; set; }

    public string TaskTypeName { get; set; } = null!;

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();
}

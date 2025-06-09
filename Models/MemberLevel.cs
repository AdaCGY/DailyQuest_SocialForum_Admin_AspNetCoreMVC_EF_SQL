using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberLevel
{
    public int LevelId { get; set; }

    public string LevelName { get; set; } = null!;

    public string? LevelDescription { get; set; }

    public int? ExperienceNeeded { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}

using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}

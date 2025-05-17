using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberAndVirtualRole
{
    public int MemberId { get; set; }

    public int RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual VirtualRole Role { get; set; } = null!;
}

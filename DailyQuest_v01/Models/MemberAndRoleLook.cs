using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberAndRoleLook
{
    public int MemberId { get; set; }

    public int LookId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual RoleLook Look { get; set; } = null!;
}

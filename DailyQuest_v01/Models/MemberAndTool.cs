using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberAndTool
{
    public int MemberId { get; set; }

    public int ToolId { get; set; }

    public bool IsGift { get; set; }

    public int? OtherMemberId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Member? OtherMember { get; set; }

    public virtual Tool Tool { get; set; } = null!;
}

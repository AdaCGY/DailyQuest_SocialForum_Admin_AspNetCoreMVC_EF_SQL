using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberCheckOut
{
    public int MemberId { get; set; }

    public int ToolId { get; set; }

    public int TotalPoint { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Tool Tool { get; set; } = null!;
}

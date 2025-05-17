using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberFriendship
{
    public int MemberIdA { get; set; }

    public int MemberIdB { get; set; }

    public int? StatusId { get; set; }

    public DateTime? RequestTime { get; set; }

    public virtual Member MemberIdANavigation { get; set; } = null!;

    public virtual Member MemberIdBNavigation { get; set; } = null!;

    public virtual FriendRequestStatus? Status { get; set; }
}

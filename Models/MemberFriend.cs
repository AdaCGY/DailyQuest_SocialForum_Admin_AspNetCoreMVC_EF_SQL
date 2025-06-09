using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberFriend
{
    public int MemberIdA { get; set; }

    public int MemberIdB { get; set; }

    public int? FriendTypeId { get; set; }

    public DateTime? RequestTime { get; set; }

    public virtual FriendType? FriendType { get; set; }

    public virtual Member MemberIdANavigation { get; set; } = null!;

    public virtual Member MemberIdBNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class FriendRequestStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<MemberFriendship> MemberFriendships { get; set; } = new List<MemberFriendship>();
}

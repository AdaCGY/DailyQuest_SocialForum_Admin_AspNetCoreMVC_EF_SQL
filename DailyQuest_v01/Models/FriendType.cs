using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class FriendType
{
    public int FriendTypeId { get; set; }

    public string FriendTypeName { get; set; } = null!;

    public virtual ICollection<MemberFriend> MemberFriends { get; set; } = new List<MemberFriend>();
}

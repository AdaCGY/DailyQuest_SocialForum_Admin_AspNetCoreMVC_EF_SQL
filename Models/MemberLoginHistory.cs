using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class MemberLoginHistory
{
    public int LogId { get; set; }

    public int MemberId { get; set; }

    public DateTime LoginTime { get; set; }

    public string? IpAddress { get; set; }

    public string? DeviceInfo { get; set; }

    public bool LoginSuccessFlag { get; set; }

    public virtual Member Member { get; set; } = null!;
}

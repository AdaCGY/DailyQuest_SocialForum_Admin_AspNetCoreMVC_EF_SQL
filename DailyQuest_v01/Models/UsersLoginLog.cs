using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class UsersLoginLog
{
    public int LoginId { get; set; }

    public int? UserId { get; set; }

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public string? IpAddress { get; set; }

    public virtual User? User { get; set; }
}

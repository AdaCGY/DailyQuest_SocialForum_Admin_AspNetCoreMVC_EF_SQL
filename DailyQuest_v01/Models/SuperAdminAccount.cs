using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class SuperAdminAccount
{
    public int SuperAdminId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public bool Is2Faenabled { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public int? PermissionId { get; set; }

    public virtual SuperAdminPermission? Permission { get; set; }

    public virtual ICollection<SuperAdminLoginLog> SuperAdminLoginLogs { get; set; } = new List<SuperAdminLoginLog>();
}

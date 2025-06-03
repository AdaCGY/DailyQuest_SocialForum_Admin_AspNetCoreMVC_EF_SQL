using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public bool Is2Faenabled { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public int Status { get; set; }

    public virtual ICollection<UsersLoginLog> UsersLoginLogs { get; set; } = new List<UsersLoginLog>();

    public virtual ICollection<UsersRole> Permissions { get; set; } = new List<UsersRole>();
}

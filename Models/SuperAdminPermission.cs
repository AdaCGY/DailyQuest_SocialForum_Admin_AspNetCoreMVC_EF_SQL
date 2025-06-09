using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class SuperAdminPermission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string? PermissionDescription { get; set; }

    public virtual ICollection<SuperAdminAccount> SuperAdminAccounts { get; set; } = new List<SuperAdminAccount>();
}

using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class VirtualRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? RoleDescription { get; set; }

    public byte[]? RolePhoto { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }
}

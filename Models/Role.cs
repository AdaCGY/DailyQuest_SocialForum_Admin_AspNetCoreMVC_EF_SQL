﻿using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}

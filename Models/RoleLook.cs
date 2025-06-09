using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class RoleLook
{
    public int LookId { get; set; }

    public string LookName { get; set; } = null!;

    public string? LookDescription { get; set; }

    public byte[]? LookPhoto { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }
}

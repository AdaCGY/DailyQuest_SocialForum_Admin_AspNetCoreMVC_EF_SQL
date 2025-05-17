using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class ScreenLook
{
    public int ScreenId { get; set; }

    public string ScreenName { get; set; } = null!;

    public string? ScreenDescription { get; set; }

    public byte[]? ScreenPhoto { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }
}

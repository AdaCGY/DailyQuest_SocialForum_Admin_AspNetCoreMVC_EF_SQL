using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class ShopAdmin
{
    public int AdminId { get; set; }

    public int MemberId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Member Member { get; set; } = null!;
}

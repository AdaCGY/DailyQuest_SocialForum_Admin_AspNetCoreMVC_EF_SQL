using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Member User { get; set; } = null!;
}

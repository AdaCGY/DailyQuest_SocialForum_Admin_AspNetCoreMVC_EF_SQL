using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class UserAndTitleR
{
    public int UserId { get; set; }

    public int TitleId { get; set; }

    public DateTime FinishDate { get; set; }

    public virtual Title Title { get; set; } = null!;

    public virtual Member User { get; set; } = null!;
}

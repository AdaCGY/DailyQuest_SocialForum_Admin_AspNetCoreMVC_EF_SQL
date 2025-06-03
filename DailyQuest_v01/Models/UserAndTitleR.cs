using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class UserAndTitleR
{
    public int MemberId { get; set; }

    public int TitleId { get; set; }

    public DateTime FinishDate { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}

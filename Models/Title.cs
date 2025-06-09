using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Title
{
    public int TitleId { get; set; }

    public int TitleTypeId { get; set; }

    public string TitleName { get; set; } = null!;

    public DateTime FinishDate { get; set; }

    public int Points { get; set; }

    public byte[] Image { get; set; } = null!;

    public int? ToolId { get; set; }

    public virtual TitleType TitleType { get; set; } = null!;

    public virtual Tool? Tool { get; set; }

    public virtual ICollection<UserAndTitleR> UserAndTitleRs { get; set; } = new List<UserAndTitleR>();
}

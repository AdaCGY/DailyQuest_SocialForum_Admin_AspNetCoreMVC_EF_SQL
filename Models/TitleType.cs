using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class TitleType
{
    public int TitleTypeId { get; set; }

    public string TitleTypeName { get; set; } = null!;

    public string TitleCondition { get; set; } = null!;

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}

using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}

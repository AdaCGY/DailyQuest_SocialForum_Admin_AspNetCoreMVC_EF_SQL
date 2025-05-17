using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class PostCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

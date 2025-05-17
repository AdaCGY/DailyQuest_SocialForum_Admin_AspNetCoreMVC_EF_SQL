using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Member User { get; set; } = null!;
}

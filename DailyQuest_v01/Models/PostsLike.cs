using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class PostsLike
{
    public int LikesId { get; set; }

    public int MemberId { get; set; }

    public int PostId { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}

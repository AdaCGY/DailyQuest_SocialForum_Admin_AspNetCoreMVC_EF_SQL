using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class CommentsLike
{
    public int LikesId { get; set; }

    public int MemberId { get; set; }

    public int CommentId { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}

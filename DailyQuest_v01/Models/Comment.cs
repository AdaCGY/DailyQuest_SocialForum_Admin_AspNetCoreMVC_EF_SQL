using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public int? ParentCommentId { get; set; }

    public DateTime CommentedAt { get; set; }

    public int MemberId { get; set; }

    public string CommentsContent { get; set; } = null!;

    public virtual ICollection<CommentsLike> CommentsLikes { get; set; } = new List<CommentsLike>();

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Member Member { get; set; } = null!;

    public virtual Comment? ParentComment { get; set; }

    public virtual Post Post { get; set; } = null!;
}

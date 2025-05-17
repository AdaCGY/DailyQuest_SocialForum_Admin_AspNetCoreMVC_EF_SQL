using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public string PostsContent { get; set; } = null!;

    public byte[]? PostImage { get; set; }

    public virtual PostCategory Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<PostsLike> PostsLikes { get; set; } = new List<PostsLike>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Member User { get; set; } = null!;
}

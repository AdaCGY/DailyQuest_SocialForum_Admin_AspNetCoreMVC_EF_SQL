using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyQuest_v01.Models;

public partial class PostCategory
{
    public int CategoryId { get; set; }

    [Display(Name = "類別名稱")]
    [Required(ErrorMessage = "請輸入類別名稱")]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}

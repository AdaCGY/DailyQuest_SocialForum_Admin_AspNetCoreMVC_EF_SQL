using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyQuest_v01.Models;

public partial class ReportCategory
{
    public int ReportCategoryId { get; set; }

    [Display(Name = "檢舉項目名稱")]
    [Required(ErrorMessage = "請輸入類別名稱")]
    public string ReportCategoryName { get; set; } = null!;
    [Display(Name = "檢舉項目內容說明")]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}

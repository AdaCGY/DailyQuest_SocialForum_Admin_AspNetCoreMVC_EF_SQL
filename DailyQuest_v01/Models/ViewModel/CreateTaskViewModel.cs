using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DailyQuest_v01.Models.ViewModel
{
    public class CreateTaskViewModel
    {
        [Display(Name = "任務類型")]
        public string TaskTypeName { get; set; } = null!;
        [Display(Name = "任務標籤")]
        public string TaskLabelName { get; set; } = null!;
        [Required(ErrorMessage = "任務內容必填")]
        [StringLength(15)]
        [Display(Name = "任務內容")]
        public string TaskContent { get; set; } = null!;
        [Required(ErrorMessage = "設定完成日必填")]
        [Display(Name = "設定完成日")]
        public DateTime ExpectDate { get; set; }
        [Required(ErrorMessage = "週期設定必填")]
        [Display(Name = "週期設定")]
        public string? SetPeriod { get; set; }
        [Display(Name = "發布時間")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "任務結果")]
        public string TaskResultName { get; set; } = null!;

    }
}

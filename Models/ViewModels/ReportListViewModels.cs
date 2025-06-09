namespace DailyQuest_v01.Models.ViewModels
{
    public class ReportListViewModels
    {
        public int ReportId { get; set; }
        public string ReportCategoryName { get; set; } = null!;

        public int MemberId { get; set; } // 檢舉者 ID

        public int TargetMemberId { get; set; } // 被檢舉的貼文之作者 ID

        public int ReportCategoryId { get; set; }

        public int PostId { get; set; }

        public DateTime ReportedAt { get; set; }

        public string ReportContent { get; set; } = null!;

        public int? AdminId { get; set; }

        public DateTime? ProcessedAt { get; set; }

        public string? AdminComment { get; set; }

        public string Status { get; set; } = null!;
        public string StatusName { get; set; } = null!;
    }
}

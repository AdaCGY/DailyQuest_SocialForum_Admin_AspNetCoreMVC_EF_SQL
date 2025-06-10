
namespace DailyQuest_v01.Models.ViewModels
{
    public class ReportsManageViewModels
    {
        public IEnumerable<ReportListViewModels> Reports { get; set; } = null!;
        // 分頁用
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        // 篩選條件
        public string? SelectedStatus { get; set; }
        public int? SelectedCategoryId { get; set; } 
    }
}

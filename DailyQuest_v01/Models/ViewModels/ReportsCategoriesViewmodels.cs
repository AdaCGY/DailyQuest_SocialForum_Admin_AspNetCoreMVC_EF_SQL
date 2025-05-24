using System.ComponentModel;

namespace DailyQuest_v01.Models.ViewModels
{
    public class ReportsCategoriesViewmodels
    {
        [DisplayName("檢舉類別編號")]
        public int ReportCategoryId { get; set; }

        [DisplayName("文章類別")]
        public string ReportCategoryName { get; set; }

        [DisplayName("類別說明")]
        public string? Description { get; set; }

        [DisplayName("創建時間")]
        public DateTime CreatedAt { get; set; }
    }
}

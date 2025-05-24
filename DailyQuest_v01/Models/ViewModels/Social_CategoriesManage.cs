using DailyQuest_v01.Models;

namespace DailyQuest_v01.Models.ViewModels
{
    public class Social_CategoriesManage
    {
        public required IEnumerable<PostCategory> PostCategories { get; set; }
        public required IEnumerable<ReportCategory> ReportCategories { get; set; }

    }
}

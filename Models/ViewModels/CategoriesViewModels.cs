namespace DailyQuest_v01.Models.ViewModels
{
    public class CategoriesViewModels
    {
        public IEnumerable<PostCategoriesViewModels> PostCategories { get; set; }
        public IEnumerable<ReportsCategoriesViewmodels> ReportCategories { get; set; }
    }
}

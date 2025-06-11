using DailyQuest_v01.Models;
using System.ComponentModel;

namespace DailyQuest_v01.Models.ViewModels
{
    public class PostCategoriesViewModels
    {
        [DisplayName("文章類別編號")]
        public int CategoryId { get; set; }

        [DisplayName("文章類別")]
        public string CategoryName { get; set; }


    }
}

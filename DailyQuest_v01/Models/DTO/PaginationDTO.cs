using DailyQuest_v01.Models.ViewModel;

namespace DailyQuest_v01.Models.DTO
{
    public class PaginationDTO
    {
        public required List<CreateTaskDTO> AllTasks { get; set; }
        public int CurrentPage { get; set; }
    }
}

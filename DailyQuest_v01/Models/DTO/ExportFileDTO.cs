using DailyQuest_v01.Models.ViewModel;
using Microsoft.Net.Http.Headers;

namespace DailyQuest_v01.Models.DTO
{
    public class ExportFileDTO
    {
        private static readonly List<string> DefaultTitle = new List<string>(){
                "任務類型", "任務標籤", "任務內容", "設定完成日", "週期設定", "發布時間", "任務結果"};
        public List<string> HeaderName { get; set; } = DefaultTitle;
        public required List<CreateTaskDTO> AllTasks { get; set; }
    }
}

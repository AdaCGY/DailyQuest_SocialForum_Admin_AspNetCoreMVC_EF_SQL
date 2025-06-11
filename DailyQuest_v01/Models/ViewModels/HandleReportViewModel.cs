using Microsoft.Extensions.Diagnostics.HealthChecks;
using NuGet.Common;

namespace DailyQuest_v01.Models.ViewModels
{
    public class HandleReportViewModel
    {
        public string title { get; set; } = null!;
        public string postContent { get; set; } = null!;
        public int memberID { get; set; }
        public string memberName { get; set; } = null!;
        public int reportId { get; set; }
    }
}

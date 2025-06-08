using DailyQuest_v01.Models;
using DailyQuest_v01.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyQuest_v01.Controllers
{
    public class ReportManageController : Controller
    {
        private readonly DailyQuestDbContext _context;
        public ReportManageController(DailyQuestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ReportsManageViewModels
            {
                Reports = await _context.Reports
                .Include(r => r.Post).Include(r => r.ReportCategory).Select(r => new ReportListViewModels
                {
                    ReportId = r.ReportId,
                    MemberId = r.MemberId,
                    TargetMemberId = r.Post.MemberId,
                    ReportCategoryId = r.ReportCategoryId,
                    ReportCategoryName = r.ReportCategory.ReportCategoryName,
                    PostId = r.PostId,
                    ReportedAt = r.ReportedAt,
                    ReportContent = r.ReportContent,
                    AdminId = r.AdminId,
                    ProcessedAt = r.ProcessedAt,
                    AdminComment = r.AdminComment,
                    Status = r.Status,
                    StatusName = r.Status == "Pending" ? "待處理"
                    : r.Status == "Approved" ? "已處理"
                    : r.Status == "Rejected" ? "檢舉未成立"
                    : "狀態不明，請檢查"
                }).ToListAsync()
            };
            return PartialView("~/Views/ReportManage/PartialViews/_ReportManageIndexPartial.cshtml",viewModel);
        }
    }
}

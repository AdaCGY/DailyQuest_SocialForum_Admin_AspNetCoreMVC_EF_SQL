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

        [HttpGet]
        public async Task<IActionResult> HandleReport(int id)
        {
            var report = await _context.Reports
            .Include(r => r.Post)
            .ThenInclude(p => p.Member)
            .FirstOrDefaultAsync(r => r.ReportId == id);

            if (report == null) return Content("找不到資料");

            var vm = new HandleReportViewModel
            {
                reportId = report.ReportId,
                title = report.Post.Title,
                postContent = report.Post.PostsContent,
                memberID = report.Post.MemberId,
                memberName = report.Post.Member.Username
            };

            return PartialView("~/Views/ReportManage/PartialViews/_HandleReportPartial.cshtml", vm);
        }
        [HttpPost]
        public IActionResult HandleReport(int reportId, string result, string comment)
        {
            var report = _context.Reports.FirstOrDefault(r => r.ReportId == reportId);
            if (report == null) return Json(new { success = false, message = "找不到檢舉資料" });

            report.Status = result;
            report.AdminComment = comment;
            report.ProcessedAt = DateTime.Now;
            report.AdminId = 1; // ← 這裡你可以用登入者資訊填上

            _context.SaveChanges();

            return Json(new { success = "結案完成" });
        }
    }
}

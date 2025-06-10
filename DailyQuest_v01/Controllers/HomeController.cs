using System.Diagnostics;
using DailyQuest_v01.Models;
using DailyQuest_v01.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyQuest_v01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DailyQuestDbContext _context;
        public HomeController(ILogger<HomeController> logger,DailyQuestDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SocialIndex()
        {
            var admin = await _context.Admins
        .Include(a => a.Member)
        .FirstOrDefaultAsync(); // 假設只有一位 Admin

            bool hasPendingReports = await _context.Reports.AnyAsync(r => r.Status == "Pending");
            ViewBag.HasPendingReports = hasPendingReports;

            // 統計資料
            var today = DateTime.Today;

            // 今日貼文總數
            int todayPostCount = await _context.Posts
                .CountAsync(p => p.CreatedAt >= today);

            // 今日檢舉數量
            int todayReportCount = await _context.Reports
                .CountAsync(r => r.ReportedAt >= today);

            // 尚未處理的檢舉數量
            int pendingReportCount = await _context.Reports
                .CountAsync(r => r.Status == "Pending");

            ViewBag.TodayPostCount = todayPostCount;
            ViewBag.TodayReportCount = todayReportCount;
            ViewBag.PendingReportCount = pendingReportCount;

            return View(admin);
        }


        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var viewmodel = new CategoriesViewModels
            {
                PostCategories = await _context.PostCategories.OrderBy(i=>i.CategoryId).Select(c => new PostCategoriesViewModels
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).ToListAsync(),

                ReportCategories = await _context.ReportCategories.Select(r => new ReportsCategoriesViewmodels
                {
                    ReportCategoryId = r.ReportCategoryId,
                    ReportCategoryName = r.ReportCategoryName,
                    Description = r.Description,
                    CreatedAt = r.CreatedAt
                }).ToListAsync(),
            };
            return PartialView("~/Views/Home/Partials/_CategoriesPartial.cshtml", viewmodel);
        }

        [HttpGet] //新增貼文類別
        public IActionResult CreatePostCategory()
        {
            return PartialView("~/Views/Home/Partials/_CreatePostCategoryPartial.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostCategory(PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                if (_context.PostCategories.Any(c => c.CategoryName == postCategory.CategoryName && c.CategoryId != postCategory.CategoryId)) // 檢查是否有重複的類別名稱
                {
                    return Json(new { success = false, message = "類別名稱已存在，請重新命名" });
                }
                _context.PostCategories.Add(postCategory);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "新增成功" });
            }

            return Json(new { success = false, message = "新增失敗" });
        }

        [HttpGet] //編輯貼文類別
        public async Task<IActionResult> EditPostCategory(int id)
        {
            var postCategory = await _context.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound(new { success = false, message = "找不到資料" });
            }
            return PartialView("~/Views/Home/Partials/_EditPostCategoryPartial.cshtml", postCategory);
        }

        [HttpPost] 
        public async Task<IActionResult> EditPostCategory(PostCategory postCategories)
        {
            if (ModelState.IsValid) // 驗證資料是否正確
            {
                if(_context.PostCategories.Any(c=>c.CategoryName == postCategories.CategoryName && c.CategoryId != postCategories.CategoryId)) // 檢查是否有重複的類別名稱
                {
                    return Json(new { success = false, message = "類別名稱已存在，請重新命名" });
                }
                _context.PostCategories.Update(postCategories); // 更新這筆分類資料
                await _context.SaveChangesAsync(); // 寫入資料庫
                return Json(new { success = true, message = "編輯成功" });
            }
            return Json(new { success = false, message = "編輯失敗" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (_context.ReportCategories.Any(c => c.ReportCategoryName == reportCategory.ReportCategoryName)) // 檢查是否有重複的類別名稱
                {
                    return Json(new { success = false, message = "檢舉類別已存在，請重新命名" });
                }
                _context.ReportCategories.Add(reportCategory);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "新增成功",
                    data = new
                    {
                        id = reportCategory.ReportCategoryId,
                        name = reportCategory.ReportCategoryName
                    }
                });
            }

            return Json(new { success = false, message = "新增失敗" });
        
        }

        [HttpGet] //編輯檢舉類別
        public async Task<IActionResult> EditReportCategory(int id)
        {
            var reportCategory = await _context.ReportCategories.FindAsync(id);
            if (reportCategory == null)
            {
                return NotFound(new { success = false, message = "找不到資料" });
            }
            return PartialView("~/Views/Home/Partials/_EditReportCategoryPartial.cshtml", reportCategory);
        }

        [HttpPost] 
        public async Task<IActionResult> EditReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid) // 驗證資料是否正確
            {
                if(_context.ReportCategories.Any(c => c.ReportCategoryName == reportCategory.ReportCategoryName && c.ReportCategoryId != reportCategory.ReportCategoryId)) // 檢查是否有重複的類別名稱
                {
                    return Json(new { success = false, message = "檢舉類別已存在，請重新命名" });
                }
                _context.ReportCategories.Update(reportCategory); // 更新這筆分類資料
                await _context.SaveChangesAsync(); // 寫入資料庫
                return Json(new { success = true, message = "編輯成功" });
            }
            return Json((new { success = false, message = "編輯失敗" }));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

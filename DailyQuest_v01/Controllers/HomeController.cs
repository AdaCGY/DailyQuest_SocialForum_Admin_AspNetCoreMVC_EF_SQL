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

        public IActionResult SocialIndex()
        {
            return View();
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
            return View(viewmodel);
        }

        [HttpGet] //新增貼文類別
        public IActionResult CreatePostCategory()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> CreatePostCategory(PostCategory postCategories)
        {
            if (ModelState.IsValid) // 驗證資料是否正確
            {
                _context.PostCategories.Add(postCategories);// 新增這筆分類資料
                await _context.SaveChangesAsync(); // 寫入資料庫
                return RedirectToAction("Categories"); //導回管理主頁
            }
            return NoContent();
        }

        [HttpGet] //編輯貼文類別
        public async Task<IActionResult> EditPostCategory(int id)
        {
            var postCategory = await _context.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            return View(postCategory);
        }

        [HttpPost] 
        public async Task<IActionResult> EditPostCategory(PostCategory postCategories)
        {
            if (ModelState.IsValid) // 驗證資料是否正確
            {
                _context.PostCategories.Update(postCategories); // 更新這筆分類資料
                await _context.SaveChangesAsync(); // 寫入資料庫
                return RedirectToAction("Categories"); //導回管理主頁
            }
            return NoContent();
        }

        [HttpGet] //新增檢舉類別
        public IActionResult CreateReportCategory()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> CreateReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid) // 驗證資料是否正確
            {
                _context.ReportCategories.Add(reportCategory); // 新增這筆分類資料
                await _context.SaveChangesAsync(); // 寫入資料庫
                return RedirectToAction("Categories"); //導回管理主頁
            }
            return NoContent();
        }

        [HttpGet] //編輯檢舉類別
        public async Task<IActionResult> EditReportCategory(int id)
        {
            var reportCategory = await _context.ReportCategories.FindAsync(id);
            if (reportCategory == null)
            {
                return NotFound();
            }
            return View(reportCategory);
        }

        [HttpPost] 
        public async Task<IActionResult> EditReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid) // 驗證資料是否正確
            {
                _context.ReportCategories.Update(reportCategory); // 更新這筆分類資料
                await _context.SaveChangesAsync(); // 寫入資料庫
                return RedirectToAction("Categories"); //導回管理主頁
            }
            return NoContent();
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

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

        [HttpGet]
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

        //[HttpPost]
        //public async Task<IActionResult> CreateReportCategory()
        //{

        //}

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

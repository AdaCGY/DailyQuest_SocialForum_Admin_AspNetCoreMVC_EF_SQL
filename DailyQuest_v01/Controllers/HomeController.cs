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
        .FirstOrDefaultAsync(); // ���]�u���@�� Admin

            bool hasPendingReports = await _context.Reports.AnyAsync(r => r.Status == "Pending");
            ViewBag.HasPendingReports = hasPendingReports;

            // �έp���
            var today = DateTime.Today;

            // ����K���`��
            int todayPostCount = await _context.Posts
                .CountAsync(p => p.CreatedAt >= today);

            // �������|�ƶq
            int todayReportCount = await _context.Reports
                .CountAsync(r => r.ReportedAt >= today);

            // �|���B�z�����|�ƶq
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

        [HttpGet] //�s�W�K�����O
        public IActionResult CreatePostCategory()
        {
            return PartialView("~/Views/Home/Partials/_CreatePostCategoryPartial.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostCategory(PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                if (_context.PostCategories.Any(c => c.CategoryName == postCategory.CategoryName && c.CategoryId != postCategory.CategoryId)) // �ˬd�O�_�����ƪ����O�W��
                {
                    return Json(new { success = false, message = "���O�W�٤w�s�b�A�Э��s�R�W" });
                }
                _context.PostCategories.Add(postCategory);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "�s�W���\" });
            }

            return Json(new { success = false, message = "�s�W����" });
        }

        [HttpGet] //�s��K�����O
        public async Task<IActionResult> EditPostCategory(int id)
        {
            var postCategory = await _context.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound(new { success = false, message = "�䤣����" });
            }
            return PartialView("~/Views/Home/Partials/_EditPostCategoryPartial.cshtml", postCategory);
        }

        [HttpPost] 
        public async Task<IActionResult> EditPostCategory(PostCategory postCategories)
        {
            if (ModelState.IsValid) // ���Ҹ�ƬO�_���T
            {
                if(_context.PostCategories.Any(c=>c.CategoryName == postCategories.CategoryName && c.CategoryId != postCategories.CategoryId)) // �ˬd�O�_�����ƪ����O�W��
                {
                    return Json(new { success = false, message = "���O�W�٤w�s�b�A�Э��s�R�W" });
                }
                _context.PostCategories.Update(postCategories); // ��s�o���������
                await _context.SaveChangesAsync(); // �g�J��Ʈw
                return Json(new { success = true, message = "�s�覨�\" });
            }
            return Json(new { success = false, message = "�s�襢��" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (_context.ReportCategories.Any(c => c.ReportCategoryName == reportCategory.ReportCategoryName)) // �ˬd�O�_�����ƪ����O�W��
                {
                    return Json(new { success = false, message = "���|���O�w�s�b�A�Э��s�R�W" });
                }
                _context.ReportCategories.Add(reportCategory);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "�s�W���\",
                    data = new
                    {
                        id = reportCategory.ReportCategoryId,
                        name = reportCategory.ReportCategoryName
                    }
                });
            }

            return Json(new { success = false, message = "�s�W����" });
        
        }

        [HttpGet] //�s�����|���O
        public async Task<IActionResult> EditReportCategory(int id)
        {
            var reportCategory = await _context.ReportCategories.FindAsync(id);
            if (reportCategory == null)
            {
                return NotFound(new { success = false, message = "�䤣����" });
            }
            return PartialView("~/Views/Home/Partials/_EditReportCategoryPartial.cshtml", reportCategory);
        }

        [HttpPost] 
        public async Task<IActionResult> EditReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid) // ���Ҹ�ƬO�_���T
            {
                if(_context.ReportCategories.Any(c => c.ReportCategoryName == reportCategory.ReportCategoryName && c.ReportCategoryId != reportCategory.ReportCategoryId)) // �ˬd�O�_�����ƪ����O�W��
                {
                    return Json(new { success = false, message = "���|���O�w�s�b�A�Э��s�R�W" });
                }
                _context.ReportCategories.Update(reportCategory); // ��s�o���������
                await _context.SaveChangesAsync(); // �g�J��Ʈw
                return Json(new { success = true, message = "�s�覨�\" });
            }
            return Json((new { success = false, message = "�s�襢��" }));
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

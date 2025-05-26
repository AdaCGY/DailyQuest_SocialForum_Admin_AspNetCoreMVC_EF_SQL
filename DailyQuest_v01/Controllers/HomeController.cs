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

        [HttpGet] //�s�W�K�����O
        public IActionResult CreatePostCategory()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> CreatePostCategory(PostCategory postCategories)
        {
            if (ModelState.IsValid) // ���Ҹ�ƬO�_���T
            {
                _context.PostCategories.Add(postCategories);// �s�W�o���������
                await _context.SaveChangesAsync(); // �g�J��Ʈw
                return RedirectToAction("Categories"); //�ɦ^�޲z�D��
            }
            return NoContent();
        }

        [HttpGet] //�s��K�����O
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
            if (ModelState.IsValid) // ���Ҹ�ƬO�_���T
            {
                _context.PostCategories.Update(postCategories); // ��s�o���������
                await _context.SaveChangesAsync(); // �g�J��Ʈw
                return RedirectToAction("Categories"); //�ɦ^�޲z�D��
            }
            return NoContent();
        }

        [HttpGet] //�s�W���|���O
        public IActionResult CreateReportCategory()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> CreateReportCategory(ReportCategory reportCategory)
        {
            reportCategory.CreatedAt = DateTime.Now;
            if (ModelState.IsValid) // ���Ҹ�ƬO�_���T
            {
                _context.ReportCategories.Add(reportCategory); // �s�W�o���������
                await _context.SaveChangesAsync(); // �g�J��Ʈw
                return RedirectToAction("Categories"); //�ɦ^�޲z�D��
            }
            return NoContent();
        }

        [HttpGet] //�s�����|���O
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
            if (ModelState.IsValid) // ���Ҹ�ƬO�_���T
            {
                _context.ReportCategories.Update(reportCategory); // ��s�o���������
                await _context.SaveChangesAsync(); // �g�J��Ʈw
                return RedirectToAction("Categories"); //�ɦ^�޲z�D��
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

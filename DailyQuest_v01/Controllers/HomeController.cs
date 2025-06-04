using System.Diagnostics;
using DailyQuest_v01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuest_v01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DailyQuestDbContext _context;

        public HomeController(ILogger<HomeController> logger, DailyQuestDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //var products = _context.VirtualRoles.Where(p => p.RoleId == 1);
            //return View(products);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StoreIndex()
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

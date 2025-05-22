using DailyQuest_v01.Models;
using Microsoft.AspNetCore.Mvc;
using Task = DailyQuest_v01.Models.Task;

namespace DailyQuest_v01.Controllers
{
    public class TaskController : Controller
    {
        private readonly DailyQuestDbContext _db;
        public TaskController(DailyQuestDbContext context) {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}

using DailyQuest_v01.Models;
using DailyQuest_v01.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Diagnostics.Eventing.Reader;
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
            ModelState.Clear();
            ViewBag.tasktypename = new List<SelectListItem>
            {
                new SelectListItem { Text = "系統", Value = "系統" },
                new SelectListItem { Text = "活動", Value = "活動" },
                new SelectListItem { Text = "私人", Value = "私人" }
            };
            ViewBag.tasklabelname = new List<SelectListItem>
            {
                new SelectListItem { Text = "工作", Value = "工作" },
                new SelectListItem { Text = "自我成長", Value = "自我成長" },
                new SelectListItem { Text = "旅遊", Value = "旅遊" },
                new SelectListItem { Text = "健身", Value = "健身" },
                new SelectListItem { Text = "其他", Value = "其他" },
            };
            ViewBag.setperiod = new List<string>() { "不定期", "每日", "每月" };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel singletask)
        {
            var tasktypeid = _db.TaskTypes.FirstOrDefault(t => t.TaskTypeName == singletask.TaskTypeName);
            var tasklabelid = _db.TaskLabels.FirstOrDefault(t => t.TaskLabelName == singletask.TaskLabelName);
            if (tasktypeid == null) return BadRequest("任務類型沒有符合資料");
            if (tasklabelid == null) return BadRequest("任務標籤沒有符合資料");
            //利用關聯從B表去對應ViewModel傳入的值，此變數就會是B表
            Task _tk = new Task()
            {
                TaskTypeId = tasktypeid.TaskTypeId,
                TaskLabelId = tasklabelid.TaskLabelId,
                TaskContent = singletask.TaskContent ?? string.Empty,
                ExpectDate = Convert.ToDateTime(singletask.ExpectDate),
                SetPeriod = singletask.SetPeriod,
                CreateDate = DateTime.Now,
                Points = CalPoints(singletask.TaskTypeName ?? string.Empty),
                ToolId = 3,
                TaskResultId = 1
            };
            await _db.Tasks.AddAsync(_tk);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //計算任務完成給予的點數
        private static int CalPoints(string typeName) {
            if (typeName == "系統") return 5;
            else if (typeName == "活動") return 10;
            else return 2;
        }
        //查詢目前Task資料表裡的資料
        public async Task<IActionResult> GetAllTasks(int taskid) {
            var eachtask = await _db.Tasks
                .Include(t => t.TaskType)
                .Include(t => t.TaskLabel)
                .Include(t => t.TaskResult)
                .ToListAsync();
            var model = eachtask.Select(task => new CreateTaskViewModel
            {
                TaskId = task.TaskId,
                TaskTypeName = task.TaskType.TaskTypeName,
                TaskLabelName = task.TaskLabel.TaskLabelName,
                TaskContent = task.TaskContent,
                ExpectDate = task.ExpectDate.Date.ToString("yyyy-MM-dd"),
                SetPeriod = task.SetPeriod,
                CreateDate = task.CreateDate,
                TaskResultName = task.TaskResult.TaskResultName
            }).ToList();
            return Json(model);
        }
        //刪除單筆Task資料表裡的資料
        public async Task<IActionResult> DeleteSingleTask(int singletaskid) {
            await _db.Tasks.Where(t => t.TaskId == singletaskid).ExecuteDeleteAsync();
            return Ok(new {message = "該筆資料已刪除" });
        }
        


    }
}

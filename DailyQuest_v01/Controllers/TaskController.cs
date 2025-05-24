using DailyQuest_v01.Models;
using DailyQuest_v01.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task = DailyQuest_v01.Models.Task;

namespace DailyQuest_v01.Controllers
{
    public class TaskController : Controller
    {
        private readonly DailyQuestDbContext _db;
        public TaskController(DailyQuestDbContext context) {
            _db = context;
        }
        public IActionResult Index(){
            ViewBag.tasktypename = new SelectList(_db.TaskTypes, "TaskTypeId", "TaskTypeName");
            ViewBag.tasklabelname = new SelectList(_db.TaskLabels, "TaskLabelId", "TaskLabelName");
            ViewBag.setperiod = new List<string>() { "不定期", "每日", "每月" };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel tasks) {
            //利用關聯從B表去對應ViewModel傳入的值，此變數就會是B表
            var taskTypeName = await _db.TaskTypes.FirstOrDefaultAsync(t => t.TaskTypeName == tasks.TaskTypeName);
            var taskLabelName = await _db.TaskLabels.FirstOrDefaultAsync(t => t.TaskLabelName == tasks.TaskLabelName);
            var taskResultName = await _db.TaskResults.FirstOrDefaultAsync(t => t.TaskResultName == "待完成");
            if (taskTypeName == null) return Json("找不到任務類型");
            if (taskLabelName == null) return Json("找不到任務標籤");
            if (taskResultName == null) return Json("找不到任務結果");
            Task _tk = new Task()
            {
                TaskTypeId = taskTypeName.TaskTypeId,
                TaskLabelId = taskLabelName.TaskLabelId,
                TaskContent = tasks.TaskContent,
                SubTaskId = tasks.SubTaskId,
                ExpectDate = tasks.ExpectDate,
                SetPeriod = tasks.SetPeriod,
                CreateDate = DateTime.Now,
                Points = CalPoints(tasks.TaskTypeName),
                ToolId = CalToolId(tasks.TaskTypeName),
                TaskResultId = taskResultName.TaskResultId,
            };
            //await _db.Tasks.AddAsync(_tk);
            //await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //計算任務完成給予的點數
        private static int CalPoints(string typeName) {
            if (typeName == "系統") return 5;
            else if (typeName == "活動") return 10;
            else return 2;
        }
        //計算任務完成給予的道具
        private static int CalToolId(string typeName){
            if (typeName == "系統") return 5;
            else if (typeName == "活動") return 10;
            else return 2;
        }

    }
}

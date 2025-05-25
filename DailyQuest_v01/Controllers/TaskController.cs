using DailyQuest_v01.Models;
using DailyQuest_v01.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var model = new CreateTaskViewModel {ExpectDate= DateTime.Now};
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
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel tasks)
        {
            var tasktypeid = _db.TaskTypes.FirstOrDefault(t => t.TaskTypeName == tasks.TaskTypeName);
            var tasklabelid = _db.TaskLabels.FirstOrDefault(t => t.TaskLabelName == tasks.TaskLabelName);
            if (tasktypeid == null) return BadRequest("任務類型沒有符合資料");
            if (tasklabelid == null) return BadRequest("任務標籤沒有符合資料");
            //利用關聯從B表去對應ViewModel傳入的值，此變數就會是B表
            Task _tk = new Task()
            {
                TaskTypeId = tasktypeid.TaskTypeId,
                TaskLabelId = tasklabelid.TaskLabelId,
                TaskContent = tasks.TaskContent,
                ExpectDate = tasks.ExpectDate,
                SetPeriod = tasks.SetPeriod,
                CreateDate = DateTime.Now,
                Points = CalPoints(tasks.TaskTypeName),
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
        //tasktypename轉換成tasktypeid
        private static int TransTypeTypeName(string typeName) {
            if (typeName == "系統") return 1;
            else if (typeName == "活動") return 2;
            else return 3;
        }
        private static int TransTypeLabelName(string labelName)
        {
            if (labelName == "工作") return 1;
            else if (labelName == "自我成長") return 2;
            else if (labelName == "旅遊") return 3;
            else if (labelName == "健身") return 4;
            else return 5;
        }
        private static int TransResultName(string resultName)
        {
            if (resultName == "待完成") return 1;
            else if (resultName == "完成") return 2;
            else return 3;
        }


    }
}

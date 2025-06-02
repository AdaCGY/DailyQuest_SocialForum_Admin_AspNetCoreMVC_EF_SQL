using DailyQuest_v01.Models;
using DailyQuest_v01.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Protocol;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

namespace DailyQuest_v01.Controllers
{
    public class MissionController : Controller
    {
        private readonly DailyQuestDbContext _db;
        public MissionController(DailyQuestDbContext context) {
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
            ViewBag.setperiod = GetSetPeriodContent();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDTO singletask)
        {
            var tasktype = await _db.TaskTypes.FirstOrDefaultAsync(t => t.TaskTypeName == singletask.TaskTypeName);
            var tasklabel = await _db.TaskLabels.FirstOrDefaultAsync(t => t.TaskLabelName == singletask.TaskLabelName);
            if (tasktype == null) return BadRequest("任務類型沒有符合資料");
            if (tasklabel == null) return BadRequest("任務標籤沒有符合資料");
            //利用關聯從B表去對應ViewModel傳入的值，此變數就會是B表
            Mission _tk = new Mission()
            {
                TaskTypeId = tasktype.TaskTypeId,
                TaskLabelId = tasklabel.TaskLabelId,
                TaskContent = singletask.TaskContent ?? string.Empty,
                ExpectDate = Convert.ToDateTime(singletask.ExpectDate),
                SetPeriod = singletask.SetPeriod,
                CreateDate = DateTime.Now,
                Points = CalPoints(singletask.TaskTypeName ?? string.Empty),
                ToolId = 3,
                TaskResultId = 1
            };
            await _db.Missions.AddAsync(_tk);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //修改已存在的資料並回存至資料庫
        [HttpPost]
        public async Task<IActionResult> ReplaceTask(CreateTaskDTO singletask) {
            var target = await _db.Missions.FirstOrDefaultAsync(t => t.TaskId == singletask.TaskId);
            var tasktype = await _db.TaskTypes.FirstOrDefaultAsync(t => t.TaskTypeName == singletask.TaskTypeName);
            var tasklabel = await _db.TaskLabels.FirstOrDefaultAsync(t => t.TaskLabelName == singletask.TaskLabelName);
            var taskresult = await _db.TaskResults.FirstOrDefaultAsync(t => t.TaskResultName == singletask.TaskResultName);
            Console.WriteLine(singletask);
            if (target == null) return BadRequest("資料庫找不到相符的資料");
            if (tasktype == null) return BadRequest("任務類型沒有符合資料"); 
            if (tasklabel == null) return BadRequest("任務標籤沒有符合資料");
            if (taskresult == null) return BadRequest("任務結果沒有符合資料");
            target.TaskTypeId = tasktype.TaskTypeId;
            target.TaskLabelId = tasklabel.TaskLabelId;
            target.TaskContent = singletask.TaskContent ?? string.Empty;
            target.ExpectDate = singletask.ExpectDate;
            target.SetPeriod = singletask.SetPeriod;
            target.TaskResultId = taskresult.TaskResultId;
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
        public async Task<IActionResult> GetAllTasks() {
            var eachtask = await _db.Missions
                .Include(t => t.TaskType)
                .Include(t => t.TaskLabel)
                .Include(t => t.TaskResult)
                .ToListAsync();
            var model = eachtask.Select(task => new CreateTaskDTO
            {
                TaskId = task.TaskId,
                TaskTypeName = task.TaskType.TaskTypeName,
                TaskLabelName = task.TaskLabel.TaskLabelName,
                TaskContent = task.TaskContent,
                ExpectDate = task.ExpectDate,
                SetPeriod = task.SetPeriod,
                CreateDate = task.CreateDate,
                TaskResultName = task.TaskResult.TaskResultName
            }).ToList();
            return Json(model);
        }
        //回傳單筆資料
        public async Task<IActionResult> GetSingleTask(int singletaskid) {
            var singletask = await _db.Missions
                .Include (t => t.TaskType)
                .Include(t => t.TaskLabel)
                .Include(t => t.TaskResult)
                .FirstOrDefaultAsync(t => t.TaskId == singletaskid);
            if (singletask == null){
                return NotFound(new { message = "找不到該任務資料" });
            }
            var model = new CreateTaskDTO
            {
                TaskId = singletask.TaskId,
                TaskTypeName = singletask.TaskType.TaskTypeName,
                TaskLabelName = singletask.TaskLabel.TaskLabelName,
                TaskContent = singletask.TaskContent,
                ExpectDate = singletask.ExpectDate,
                SetPeriod = singletask.SetPeriod,
                CreateDate = singletask.CreateDate,
                TaskResultName = singletask.TaskResult.TaskResultName
            };
            return Json(model);
        }
        //刪除單筆Task資料表裡的資料
        public async Task<IActionResult> DeleteSingleTask(int singletaskid) {
            await _db.Missions.Where(t => t.TaskId == singletaskid).ExecuteDeleteAsync();
            return Ok(new {message = "該筆資料已刪除" });
        }
        public async Task<IActionResult> GetTypeName() {
            var model = await _db.TaskTypes.ToListAsync();
            return Json(model);
        }
        public async Task<IActionResult> GetLabelName() {
            var model = await _db.TaskLabels.ToListAsync();
            return Json(model); 
        }
        public async Task<IActionResult> GetResultName(){
            var model = await _db.TaskResults.ToListAsync();
            return Json(model);
        }
        public List<string> GetSetPeriodContent() {
            return new List<string>() { "不定期", "每日", "每月"};
        }
        public async Task<IActionResult> SearchKeyword(string keyword) {
            var eachtask = await _db.Missions.Include(t => t.TaskType).Include(t => t.TaskLabel).Include(t => t.TaskResult).ToListAsync();
            var model = eachtask.Where(s => s.TaskContent.Contains(keyword) && s.TaskContent != null)
                .Select(task => new CreateTaskDTO
                {
                    TaskId = task.TaskId,
                    TaskTypeName = task.TaskType.TaskTypeName,
                    TaskLabelName = task.TaskLabel.TaskLabelName,
                    TaskContent = task.TaskContent,
                    ExpectDate = task.ExpectDate,
                    SetPeriod = task.SetPeriod,
                    CreateDate = task.CreateDate,
                    TaskResultName = task.TaskResult.TaskResultName
                }).ToList();
            if (model == null) { return Json("找不到符合的資料"); }
            else { return Json(model); }
        }
        public async Task<IActionResult> Pagination(int currentpage) {
            int pagesize = 5;
            var origintasks = await _db.Missions.Include(t => t.TaskType).Include(t => t.TaskLabel).Include(t => t.TaskResult).ToListAsync();
            var onepagetasks = origintasks.Select(task => new CreateTaskDTO {
                TaskId = task.TaskId,
                TaskTypeName = task.TaskType.TaskTypeName,
                TaskLabelName = task.TaskLabel.TaskLabelName,
                TaskContent = task.TaskContent,
                ExpectDate = task.ExpectDate,
                SetPeriod = task.SetPeriod,
                CreateDate = task.CreateDate,
                TaskResultName = task.TaskResult.TaskResultName
            }).ToList().Skip((currentpage - 1) * pagesize).Take(pagesize);
            return Json(onepagetasks);
        }
    }
}

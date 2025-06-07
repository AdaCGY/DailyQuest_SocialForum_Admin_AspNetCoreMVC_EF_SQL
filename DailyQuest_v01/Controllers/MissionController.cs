using DailyQuest_v01.Models;
using DailyQuest_v01.Models.DTO;
using DailyQuest_v01.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Protocol;
using OfficeOpenXml;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            ViewBag.resultname = new List<SelectListItem>
            {
                new SelectListItem { Text = "待完成", Value = "待完成" },
                new SelectListItem { Text = "完成", Value = "完成" },
            }; ;
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
        //搜尋關鍵字
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
        //放入資料而製成分頁
        [HttpPost]
        public IActionResult Pagination([FromBody] PaginationDTO updatetasks) {
            int pagesize = 5;
            var onepagetasks = updatetasks.AllTasks.Skip((updatetasks.CurrentPage - 1) * pagesize).Take(pagesize);
            return Json(onepagetasks);
        }
        //各項欄位篩選資料
        public async Task<IActionResult> Choice(string colname, string content) {
            var eachtask = await _db.Missions.Include(t => t.TaskType).Include(t => t.TaskLabel).Include(t => t.TaskResult).ToListAsync();
            if (colname == "TaskTypeName") {
                eachtask = eachtask.Where(s => s.TaskType.TaskTypeName == content).ToList();
            }
            else if (colname == "TaskLabelName") {
                eachtask = eachtask.Where(s => s.TaskLabel.TaskLabelName == content).ToList();
            }
            else if (colname == "SetPeriod") {
                eachtask = eachtask.Where(s => s.SetPeriod == content).ToList();
            }
            else {
                eachtask = eachtask.Where(s => s.TaskResult.TaskResultName == content).ToList();
            }
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
        //匯出成excel檔案
        [HttpPost]
        public IActionResult ExportExcel([FromBody] ExportFileDTO currenttasks) {
            //必要EPPlus套件_設定非商業用途授權
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
            using (var excelfile = new ExcelPackage()){
                var workSheet = excelfile.Workbook.Worksheets.Add("AllTasks");
                var allTasks = currenttasks.AllTasks;
                var headerName = currenttasks.HeaderName;
                for (int i = 0; i < headerName.Count; i++)
                {
                    workSheet.Cells[1, i + 1].Value = headerName[i];
                }
                for (int row = 0; row < allTasks.Count; row++)
                {
                    int rowIndex = row + 2; // 第一列為標題列，內容從第二列開始
                    workSheet.Cells[rowIndex, 1].Value = allTasks[row].TaskTypeName;
                    workSheet.Cells[rowIndex, 2].Value = allTasks[row].TaskLabelName;
                    workSheet.Cells[rowIndex, 3].Value = allTasks[row].TaskContent;
                    workSheet.Cells[rowIndex, 4].Value = allTasks[row].ExpectDate.ToString("yyyy-MM-dd");
                    workSheet.Cells[rowIndex, 5].Value = allTasks[row].SetPeriod;
                    workSheet.Cells[rowIndex, 6].Value = allTasks[row].CreateDate.ToString("yyyy-MM-dd");
                    workSheet.Cells[rowIndex, 7].Value = allTasks[row].TaskResultName;
                }
                var excelBytes = excelfile.GetAsByteArray();
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}

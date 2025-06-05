using Microsoft.AspNetCore.Mvc;

namespace DailyQuest_v01.Controllers
{
    public class ReportManageController : Controller
    {
        public IActionResult Index()
        {
            return PartialView("~/Views/ReportManage/PartialViews/_ReportManageIndexPartial.cshtml");
        }
    }
}

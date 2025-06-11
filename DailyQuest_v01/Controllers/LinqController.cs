using DailyQuest_v01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuest_v01.Controllers
{
    public class LinqController : Controller
    {

        private readonly DailyQuestDbContext _context;
        public LinqController(DailyQuestDbContext context) { 
            _context = context;
        }

        public IActionResult Index()
        {
            //新增
            //VirtualRole virtualRole = new VirtualRole
            //{
            //    RoleName = "Test3", 
            //    //RoleDescription = "Test",
            //    CreatedAt = DateTime.Now,
            //};
            //_context.VirtualRoles.Add(virtualRole);
            //_context.SaveChanges();

            //修改
            //VirtualRole? virtualRole = _context.VirtualRoles.Find(9);
            //if (virtualRole != null) 
            //{ 
            //    virtualRole.RoleName = "Retest2";
            //    virtualRole.CreatedAt = DateTime.Now;
            //}
            //_context.SaveChanges();

            //刪除
            VirtualRole? virtualRole = _context.VirtualRoles.Find(9);
            if (virtualRole != null)
            {
                _context.VirtualRoles.Remove(virtualRole);
            }
            _context.SaveChanges();

            return View();
        }
    }
}

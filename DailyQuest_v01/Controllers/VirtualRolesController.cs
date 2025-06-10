using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DailyQuest_v01.Models;
using Microsoft.Extensions.Hosting;
using DailyQuest_v01.Models.ViewModel;

namespace DailyQuest_v01.Controllers
{
    public class VirtualRolesController : Controller
    {
        private readonly DailyQuestDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VirtualRolesController(DailyQuestDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: VirtualRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.VirtualRoles.ToListAsync());
        }

        // GET: VirtualRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virtualRole = await _context.VirtualRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (virtualRole == null)
            {
                return NotFound();
            }

            return View(virtualRole);
        }

        // GET: VirtualRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VirtualRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VirtualRoleViewModel virtualRole)
        {
            if (virtualRole.RolePhoto != null)
            {
                //檔案上傳的路徑
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "VirtualRoles", virtualRole.RolePhoto.FileName);
                //檔案上傳
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await virtualRole.RolePhoto.CopyToAsync(stream);
                }
            }

            VirtualRole _virtualRole = new VirtualRole()
            {
                RoleId = virtualRole.RoleId,
                RoleName = virtualRole.RoleName,
                RoleDescription = virtualRole.RoleDescription,
                RolePhoto = virtualRole.RolePhoto?.FileName,
                CreatedAt = virtualRole.CreatedAt,
                LastModified = virtualRole.LastModified
            };

            await _context.VirtualRoles.AddAsync(_virtualRole);
            await _context.SaveChangesAsync();

            return RedirectToAction("Create");
        }

        //public async Task<IActionResult> Create([Bind("RoleId,RoleName,RoleDescription,RolePhoto,CreatedAt,LastModified")] VirtualRole virtualRole)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(virtualRole);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(virtualRole);
        //}

        // GET: VirtualRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virtualRole = await _context.VirtualRoles.FindAsync(id);
            if (virtualRole == null)
            {
                return NotFound();
            }
            return View(virtualRole);
        }

        // POST: VirtualRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,RoleDescription,RolePhoto,CreatedAt,LastModified")] VirtualRole virtualRole)
        {
            if (id != virtualRole.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(virtualRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VirtualRoleExists(virtualRole.RoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(virtualRole);
        }

        // GET: VirtualRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virtualRole = await _context.VirtualRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (virtualRole == null)
            {
                return NotFound();
            }

            return View(virtualRole);
        }

        // POST: VirtualRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var virtualRole = await _context.VirtualRoles.FindAsync(id);
            if (virtualRole != null)
            {
                _context.VirtualRoles.Remove(virtualRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VirtualRoleExists(int id)
        {
            return _context.VirtualRoles.Any(e => e.RoleId == id);
        }
    }
}

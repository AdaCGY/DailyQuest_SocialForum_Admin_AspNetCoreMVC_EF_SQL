using DailyQuest_v01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuest_v01.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DailyQuestDbContext _context;
        public APIController(ILogger<HomeController> logger, DailyQuestDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostCategory(int id)
        {
            var postCategory = await _context.PostCategories.FindAsync(id);

            if (postCategory == null)
            {
                return NotFound(new { success = false, message = "找不到資料" });
            }

            _context.PostCategories.Remove(postCategory);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "刪除成功" });
        }
    }
}

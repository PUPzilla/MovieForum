using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Data;
using MovieForum2.Models;

namespace MovieForum2.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieForum2Context _context;

        public HomeController(MovieForum2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get all discussions
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
    }
}

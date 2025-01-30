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
            var discussions = await _context.Discussion.ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> DiscussionImage(int id)
        {
            // Get by ID
            var image = await _context.Discussion.FirstOrDefaultAsync(m => m.DiscussionId == id);

            return View(image);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Data;
using MovieForum2.Models;

namespace MovieForum2.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieForum2Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(MovieForum2Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Display all discussions
        public async Task<IActionResult> Index()
        {
            // Get all discussions
            var discussions = await _context.Discussion
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.Comments)
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            discussion.Comments = discussion.Comments.OrderBy(c => c.CreateDate).ToList();

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the user by id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Fetch user's discussion threads (assuming a relationship with Discussion)
            var discussions = await _context.Discussion
                                            .Where(d => d.ApplicationUserId == user.Id)
                                            .OrderByDescending(d => d.CreateDate)
                                            .ToListAsync();

            // Create a ViewModel to hold both user and discussions
            var profileViewModel = new ProfileViewModel
            {
                User = user,
                Discussions = discussions
            };

            return View(profileViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
    }
}

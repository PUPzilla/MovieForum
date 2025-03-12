using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Data;
using MovieForum2.Models;

namespace MovieForum2.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MovieForum2Context _context;

        public HomeController(MovieForum2Context context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // Display all discussions
        public async Task<IActionResult> Index()
        {            
            var discussions = await _context.Discussion
                .OrderByDescending(d => d.CreateDate)
                .Include(d => d.ApplicationUser)
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
                .Include(d => d.ApplicationUser)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            discussion.Comments = discussion.Comments.OrderBy(c => c.CreateDate).ToList();

            return View(discussion);
        }

        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Retrieve user details
            var user = await _userManager.Users
                .Where(u => u.Id == id)
                .Include(u => u.Discussions)
                .Select(u => new ProfileView
                {
                    Id = u.Id,
                    Name = u.Name,
                    Location = u.Location,
                    ProfilePicture = u.ImageFilename,
                    DiscussionThreads = u.Discussions
                        .OrderByDescending(d => d.CreateDate)
                        .Select(d => new DiscussionThreadViewModel
                        {
                            Id = d.DiscussionId,
                            Title = d.Title,
                            CreatedAt = d.CreateDate,
                            ImageFilename = d.ImageFilename
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult Privacy()
        {
            return View();
        }
        
    }
}

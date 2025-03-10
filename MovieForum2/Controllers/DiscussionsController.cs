using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Data;
using MovieForum2.Models;

namespace MovieForum2.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly MovieForum2Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionsController(MovieForum2Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var discussions = await _context.Discussion
                .Include(m => m.Comments)
                .Where(m => m.ApplicationUserId == userId)
                .OrderByDescending(d => d.CreateDate)
                .ToListAsync();

            return View(discussions);
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,ImageFile")] Discussion discussion)
        {
            discussion.ApplicationUserId = _userManager.GetUserId(User);

            if (discussion.ApplicationUserId == null)
            {
                ModelState.AddModelError("", "User must be logged in to create a new discussion.");
                return View(discussion);
            }

            // Assign create date
            discussion.CreateDate = DateTime.Now;

            if (discussion.ImageFile != null)
            {
                discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", discussion.ImageFilename);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await discussion.ImageFile.CopyToAsync(fileStream);
            }

            _context.Add(discussion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion.Include("Comments").FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }
            return View(discussion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,CreateDate,ImageFile,ApplicationUserId")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            var existingDiscussion = await _context.Discussion.FindAsync(id);
            if (existingDiscussion == null)
            {
                return NotFound();
            }

            // Will keep the current image if user doesn't upload new file
            if (discussion.ImageFile != null)
            {
                discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", discussion.ImageFilename);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await discussion.ImageFile.CopyToAsync(fileStream);
            }
            else
            {
                discussion.ImageFilename = existingDiscussion.ImageFilename;
            }

            _context.Entry(existingDiscussion).CurrentValues.SetValues(discussion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion != null)
            {
                _context.Discussion.Remove(discussion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

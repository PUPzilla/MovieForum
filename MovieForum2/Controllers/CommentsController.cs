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
    public class CommentsController : Controller
    {
        private readonly MovieForum2Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(MovieForum2Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments/Create
        public IActionResult Create(int DiscussionId)
        {
            var discussion = _context.Discussion
                .FirstOrDefault(d => d.DiscussionId == DiscussionId);

            if (discussion == null)
            {
                return NotFound();
            }

            var comment = new Comment { DiscussionId = DiscussionId };

            ViewBag.DiscussionTitle = discussion.Title;
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int discussionId, [Bind("Content")] Comment comment)
        {
            var discussion = await _context.Discussion.FindAsync(discussionId);
            if (discussion == null)
            {
                return NotFound();
            }

            comment.DiscussionId = discussionId;
            comment.ApplicationUserId = _userManager.GetUserId(User);
            comment.CreateDate = DateTime.Now;

            _context.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetDiscussion", "Home", new { id = discussionId });
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}

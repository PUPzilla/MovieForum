using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CommentsController(MovieForum2Context context)
        {
            _context = context;
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

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreatedDate,DiscussionId")] Comment comment)
        {
            if (!_context.Discussion.Any(d => d.DiscussionId == comment.DiscussionId))
            {
                ModelState.AddModelError("DiscussionId", "Invalid Discussion ID.");
                ViewBag.DiscussionTitle = await _context.Discussion
                    .Where(d => d.DiscussionId == comment.DiscussionId)
                    .Select(d => d.Title)
                    .FirstOrDefaultAsync();

                return View(comment);
            }

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            ViewBag.DiscussionTitle = await _context.Discussion
                .Where(d => d.DiscussionId == comment.DiscussionId)
                .Select(d => d.Title)
                .FirstOrDefaultAsync();

            return View(comment);
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}

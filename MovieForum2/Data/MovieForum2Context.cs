using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Models;

namespace MovieForum2.Data
{
    public class MovieForum2Context : IdentityDbContext<ApplicationUser>
    {
        public MovieForum2Context (DbContextOptions<MovieForum2Context> options) 
            : base(options)
        {
        }

        public DbSet<Discussion> Discussion { get; set; } = default!;
        public DbSet<Comment> Comment { get; set; } = default!;
    }
}

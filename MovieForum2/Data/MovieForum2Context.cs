using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Models;

namespace MovieForum2.Data
{
    public class MovieForum2Context : IdentityDbContext<ApplicationUser>
    {
        public MovieForum2Context(DbContextOptions<MovieForum2Context> options)
            : base(options)
        {
        }

        public DbSet<Discussion> Discussion { get; set; } = default!;
        public DbSet<Comment> Comment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Comment -> Discussion (1 Discussion can have many Comments)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Discussion)
                .WithMany(d => d.Comments)
                .HasForeignKey(c => c.DiscussionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment -> ApplicationUser (1 User can have many Comments)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ApplicationUser)
                .WithMany()
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

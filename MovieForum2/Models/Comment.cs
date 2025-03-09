using MovieForum2.Data;
using MovieForum2.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieForum2.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        // Foreign Key (User)
        public string? ApplicationUserId { get; set; }

        // Navigation Property
        public ApplicationUser? ApplicationUser { get; set; }

        // Foreign Key (Discussion)
        [Required]
        public int DiscussionId { get; set; }

        public Discussion? Discussion { get; set; }
    }

}

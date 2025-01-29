using System.ComponentModel.DataAnnotations;

namespace MovieForum2.Models
{
    public class Comment
    {
        // Primary Key
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        // Foreign Key
        public int discussionId { get; set; }
        // Nav Property
        public Discussion? Discussion { get; set; }
    }
}

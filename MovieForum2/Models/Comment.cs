using System.ComponentModel.DataAnnotations;

namespace MovieForum2.Models
{
    public class Comment
    {
        // Primary Key
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        // Foreign Key
        public int discussionId { get; set; }
        public Discussion Discussion { get; set; }
    }
}

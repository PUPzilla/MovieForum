using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieForum2.Models
{
    public class Discussion
    {
        // Primary Key
        [Key]
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageFilename { get; set; }
        // Property for image file uploads
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        // Nav Property
        public List<Comment>? Comments { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieForum2.Models
{
    public class Discussion
    {
        // Primary Key
        [Key]
        public int DiscussionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageFilename { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
        DateTime CreateDate { get; set; } = DateTime.Now;
        // Nav Property
        public List<Comment> Comments { get; set; }
    }
}

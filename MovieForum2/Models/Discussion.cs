using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieForum2.Data;

namespace MovieForum2.Models
{
    public class Discussion
    {
        // Primary Key
        [Key]
        public int DiscussionId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;

        // Property for image file uploads
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public DateTime CreateDate { get; set; }

        //Foreign Key
        public string ApplicationUserId { get; set; } = string.Empty;

        // Nav Property
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Discussion()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}

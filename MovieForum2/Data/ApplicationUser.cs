using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MovieForum2.Models;

namespace MovieForum2.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        // Profile Picture
        [PersonalData]
        public string ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // Navigation property for user discussions
        public ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();
    }
}

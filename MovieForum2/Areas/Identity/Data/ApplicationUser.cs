using Microsoft.AspNetCore.Identity;

namespace MovieForum2.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string? Location { get; set; }

        //Profile Picture
        [PersonalData]
        public string? ImageFilename { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

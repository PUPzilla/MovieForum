using Microsoft.AspNetCore.Identity;

namespace MovieForum2.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; } = string.Empty;

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        //Profile Picture
        [PersonalData]
        public string ImageFilename { get; set; } = string.Empty;

    }
}

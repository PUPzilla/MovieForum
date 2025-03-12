using MovieForum2.Models;
using MovieForum2.Areas.Identity;
using System;
using System.Collections.Generic;

namespace MovieForum2.Models
{
    public class ProfileView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ProfilePicture { get; set; }
        public List<DiscussionThreadViewModel> DiscussionThreads { get; set; }
    }

    public class DiscussionThreadViewModel
    {
        public string ImageFilename { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieForum2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MovieForum2.Data
{
    public class MovieForum2Context : IdentityDbContext
    {
        public MovieForum2Context (DbContextOptions<MovieForum2Context> options) : base(options)
        {

        }

        public DbSet<MovieForum2.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<MovieForum2.Models.Comment> Comment { get; set; } = default!;
    }
}

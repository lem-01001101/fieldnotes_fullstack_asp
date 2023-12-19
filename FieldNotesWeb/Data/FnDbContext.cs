using FieldNotesWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FieldNotesWeb.Data
{
    public class FnDbContext : DbContext
    {
        public FnDbContext(DbContextOptions<FnDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}

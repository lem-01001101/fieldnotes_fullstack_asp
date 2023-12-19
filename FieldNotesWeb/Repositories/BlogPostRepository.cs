using FieldNotesWeb.Data;
using FieldNotesWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FieldNotesWeb.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly FnDbContext fnDbContext;
        public BlogPostRepository(FnDbContext fnDbContext)
        {
            this.fnDbContext = fnDbContext;   
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await fnDbContext.AddAsync(blogPost);
            await fnDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await fnDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                fnDbContext.BlogPosts.Remove(existingBlog);
                await fnDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await fnDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await fnDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await fnDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await fnDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageURL = blogPost.FeaturedImageURL;
                existingBlog.UrlHandle  = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await fnDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        
    }
}

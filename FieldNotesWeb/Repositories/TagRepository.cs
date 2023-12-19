using FieldNotesWeb.Data;
using FieldNotesWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FieldNotesWeb.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FnDbContext fnDbContext;

        public TagRepository(FnDbContext fnDbContext)
        {
            this.fnDbContext = fnDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await fnDbContext.Tags.AddAsync(tag);
            await fnDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await fnDbContext.Tags.FindAsync(id);

            if(existingTag != null) 
            { 
                fnDbContext.Tags.Remove(existingTag);
                await fnDbContext.SaveChangesAsync();   
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await fnDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return fnDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await fnDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await fnDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}

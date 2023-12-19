using Microsoft.AspNetCore.Mvc.Rendering;

namespace FieldNotesWeb.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }

        public String Heading { get; set; }
        public String PageTitle { get; set; }
        public String Content { get; set; }
        public String ShortDescription { get; set; }
        public String FeaturedImageURL { get; set; }
        public String UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //collect tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}

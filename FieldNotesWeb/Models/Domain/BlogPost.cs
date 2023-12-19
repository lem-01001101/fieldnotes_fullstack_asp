namespace FieldNotesWeb.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        public String Heading { get; set; }
        public String PageTitle { get; set; }
        public String Content { get; set; }
        public String ShortDescription { get; set; }
        public String FeaturedImageURL { get; set; }
        public String UrlHandle{ get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // navigation property
        public ICollection<Tag> Tags { get; set; }
    }
}

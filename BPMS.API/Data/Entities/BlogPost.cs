using BPMS.API.Data.Abstractions;

namespace BPMS.API.Data.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string Qoute { get; set; }
    }
}

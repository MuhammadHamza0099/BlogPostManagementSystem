using System.ComponentModel.DataAnnotations;

namespace BPMS.API.Data.DTOs
{
    public class BlogPostDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100, ErrorMessage = "Author cannot be longer than 100 characters")]
        public string Author { get; set; }

        [MaxLength(5000, ErrorMessage = "Content cannot be longer than 5000 characters")]
        public string Content { get; set; }

        [MaxLength(100, ErrorMessage = "Quote cannot be longer than 100 characters")]
        public string Qoute { get; set; }
    }
}

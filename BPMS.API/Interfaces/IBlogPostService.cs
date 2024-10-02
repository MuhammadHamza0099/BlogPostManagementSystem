using BPMS.API.Data.DTOs;

namespace BPMS.API.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDTO>> GetAllAsync();
        Task<BlogPostDTO> GetByIdAsync(int id);
        Task AddAsync(BlogPostDTO blogPost);
        Task<IEnumerable<BlogPostDTO>> SearchAsync(string title, string author);
    }
}

using BPMS.API.Data.DTOs;
using BPMS.Result;

namespace BPMS.API.Interfaces
{
    public interface IBlogPostService
    {
        Task<Result<IEnumerable<BlogPostDTO>>> GetAllAsync();
        Task<Result<BlogPostDTO>> GetByIdAsync(int id);
        Task<Result<BlogPostDTO>> AddAsync(BlogPostDTO blogPost);
        Task<Result<IEnumerable<BlogPostDTO>>> SearchAsync(string title, string author);
    }
}

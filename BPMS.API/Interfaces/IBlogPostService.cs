using BPMS.API.Data.Entities;

namespace BPMS.API.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task AddAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> SearchAsync(string title, string author);
    }
}

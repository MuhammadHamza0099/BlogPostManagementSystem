using BPMS.API.Data;
using BPMS.API.Data.Entities;
using BPMS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BPMS.API.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly ApplicationDbContext _context;

        public BlogPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts
                .Select(b => new BlogPost
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Qoute = b.Qoute
                })
                .ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task AddAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPost>> SearchAsync(string title, string author)
        {
            var query = _context.BlogPosts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }

            return await query.ToListAsync();
        }
    }
}

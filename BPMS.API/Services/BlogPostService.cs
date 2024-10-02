using AutoMapper;
using BPMS.API.Data;
using BPMS.API.Data.DTOs;
using BPMS.API.Data.Entities;
using BPMS.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BPMS.API.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BlogPostService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPostDTO>> GetAllAsync()
        {
            var blogPosts = await _context.BlogPosts.ToListAsync();
            return _mapper.Map<IEnumerable<BlogPostDTO>>(blogPosts);
        }

        public async Task<BlogPostDTO> GetByIdAsync(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            return _mapper.Map<BlogPostDTO>(blogPost);
        }

        public async Task AddAsync(BlogPostDTO blogPostDto)
        {
            var blogPost = _mapper.Map<BlogPost>(blogPostDto);
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostDTO>> SearchAsync(string title, string author)
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

            var blogPosts = await query.ToListAsync();
            return _mapper.Map<IEnumerable<BlogPostDTO>>(blogPosts);
        }
    }

}

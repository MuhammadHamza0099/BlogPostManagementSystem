using AutoMapper;
using BPMS.API.Data;
using BPMS.API.Data.DTOs;
using BPMS.API.Data.Entities;
using BPMS.API.Exceptions;
using BPMS.API.Extensions;
using BPMS.API.Interfaces;
using BPMS.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BPMS.API.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public BlogPostService(ApplicationDbContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<IEnumerable<BlogPostDTO>>> GetAllAsync()
        {
            const string cacheKey = "blogPostsCache";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<BlogPostDTO> blogPostDTOs))
            {
                var blogPosts = await _context.BlogPosts.ToListAsync();
                blogPostDTOs = _mapper.Map<IEnumerable<BlogPostDTO>>(blogPosts);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, blogPostDTOs, cacheOptions);
            }

            return Result<IEnumerable<BlogPostDTO>>.Success(blogPostDTOs);
        }

        public async Task<Result<BlogPostDTO>> GetByIdAsync(string id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id.FromSqid());
            if (blogPost == null)
            {
                throw new NotFoundException("Blog post not found");
            }

            var blogPostDTO = _mapper.Map<BlogPostDTO>(blogPost);
            return Result<BlogPostDTO>.Success(blogPostDTO);
        }

        public async Task<Result<BlogPostDTO>> AddAsync(BlogPostDTO blogPostDto)
        {
            // Perform input validation, throw exceptions as needed
            if (string.IsNullOrWhiteSpace(blogPostDto.Title) || string.IsNullOrWhiteSpace(blogPostDto.Author))
            {
                throw new BadRequestException("Title and Author cannot be empty.");
            }

            var blogPost = _mapper.Map<BlogPost>(blogPostDto);
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return Result<BlogPostDTO>.Success(blogPostDto, "Blog post created successfully");
        }

        public async Task<Result<IEnumerable<BlogPostDTO>>> SearchAsync(string title, string author)
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
            var blogPostDTOs = _mapper.Map<IEnumerable<BlogPostDTO>>(blogPosts);

            return Result<IEnumerable<BlogPostDTO>>.Success(blogPostDTOs);
        }
    }
}

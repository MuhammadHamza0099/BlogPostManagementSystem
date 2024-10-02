using BPMS.API.Data.DTOs;
using BPMS.API.Extensions;
using BPMS.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BPMS.API.Controllers
{
    [Route("api/BlogPosts")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        // GET: api/BlogPosts/getposts
        [HttpGet("getposts")]
        public async Task<ActionResult<IEnumerable<BlogPostDTO>>> GetBlogPosts()
        {
            var posts = await _blogPostService.GetAllAsync();
            return Ok(posts);
        }

        // GET: api/BlogPosts/getbyid/{id}
        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<BlogPostDTO>> GetBlogPost(string id)
        {
            var post = await _blogPostService.GetByIdAsync(id.FromSqid());

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST: api/BlogPosts/addpost
        [HttpPost("addpost")]
        public async Task<IActionResult> PostBlogPost(BlogPostDTO blogPostDto)
        {
            await _blogPostService.AddAsync(blogPostDto);
            return Ok(blogPostDto);
        }

        // GET: api/BlogPosts/search?title=sample&author=john
        [HttpGet("searchByTitleOrAuthor")]
        public async Task<ActionResult<IEnumerable<BlogPostDTO>>> Search(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(author))
            {
                return BadRequest("At least one search parameter (title or author) must be provided.");
            }

            var posts = await _blogPostService.SearchAsync(title, author);
            return Ok(posts);
        }
    }
}

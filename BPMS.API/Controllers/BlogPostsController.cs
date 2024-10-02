using BPMS.API.Data.Entities;
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
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
        {
            var posts = await _blogPostService.GetAllAsync();
            return Ok(posts);
        }

        // GET: api/BlogPosts/getbyid/{id}
        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(string id)
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
        public async Task<IActionResult> PostBlogPost(BlogPost blogPost)
        {
            await _blogPostService.AddAsync(blogPost);
            return Ok(blogPost); // Return the created blog post with OK status
        }


        // GET: api/BlogPosts/search?title=sample&author=john
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> Search(string? title, string? author)
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

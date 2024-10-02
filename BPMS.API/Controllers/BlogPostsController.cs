using BPMS.API.Data.DTOs;
using BPMS.API.Extensions;
using BPMS.API.Interfaces;
using BPMS.Result;
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
        public async Task<IActionResult> GetBlogPosts()
        {
            var result = await _blogPostService.GetAllAsync();

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/BlogPosts/getbyid/{id}
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetBlogPost(string id)
        {
            var result = await _blogPostService.GetByIdAsync(id.FromSqid());

            if (!result.Succeeded)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        // POST: api/BlogPosts/addpost
        [HttpPost("addpost")]
        public async Task<IActionResult> PostBlogPost([FromBody] BlogPostDTO blogPostDto)
        {
            if (!ModelState.IsValid)
            {
                // Return the validation errors to the client
                return BadRequest(Result<BlogPostDTO>.Fail(
                    "Validation failed",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                ));
            }

            var result = await _blogPostService.AddAsync(blogPostDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/BlogPosts/search?title=sample&author=john
        [HttpGet("searchByTitleOrAuthor")]
        public async Task<IActionResult> Search(string title, string author)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(author))
            {
                return BadRequest("At least one search parameter (title or author) must be provided.");
            }

            var result = await _blogPostService.SearchAsync(title, author);

            if (!result.Succeeded)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}

using BPMS.API.Controllers;
using BPMS.API.Data.DTOs;
using BPMS.API.Interfaces;
using BPMS.Result;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BPMS.Tests.Controllers
{
    public class BlogPostsControllerTests
    {
        private readonly Mock<IBlogPostService> _mockBlogPostService;
        private readonly BlogPostsController _controller;

        public BlogPostsControllerTests()
        {
            _mockBlogPostService = new Mock<IBlogPostService>();
            _controller = new BlogPostsController(_mockBlogPostService.Object);
        }

        [Fact]
        public async Task GetBlogPosts_ReturnsOkResult_WithBlogPostDTOs()
        {
            // Arrange
            var mockBlogPosts = new List<BlogPostDTO>
        {
            new BlogPostDTO { Id = "1", Title = "Post 1", Author = "Author 1" },
            new BlogPostDTO { Id = "2", Title = "Post 2", Author = "Author 2" }
        };

            _mockBlogPostService.Setup(service => service.GetAllAsync())
                                .ReturnsAsync(Result<IEnumerable<BlogPostDTO>>.Success(mockBlogPosts));

            // Act
            var result = await _controller.GetBlogPosts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Result<IEnumerable<BlogPostDTO>>>(okResult.Value);
            Assert.True(returnValue.Succeeded);
            Assert.Equal(2, returnValue.Data.Count());
        }

        [Fact]
        public async Task GetBlogPost_ReturnsOkResult_WithBlogPostDTO()
        {
            // Arrange
            var mockBlogPost = new BlogPostDTO { Id = "1", Title = "Post 1", Author = "Author 1" };
            _mockBlogPostService.Setup(service => service.GetByIdAsync(It.IsAny<string>()))
                                .ReturnsAsync(Result<BlogPostDTO>.Success(mockBlogPost));

            // Act
            var result = await _controller.GetBlogPost("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Result<BlogPostDTO>>(okResult.Value);
            Assert.True(returnValue.Succeeded);
            Assert.Equal("Post 1", returnValue.Data.Title);
        }

        [Fact]
        public async Task PostBlogPost_ReturnsOkResult_WithCreatedBlogPost()
        {
            // Arrange
            var newBlogPostDto = new BlogPostDTO { Title = "New Post", Author = "New Author" };
            _mockBlogPostService.Setup(service => service.AddAsync(It.IsAny<BlogPostDTO>()))
                                .ReturnsAsync(Result<BlogPostDTO>.Success(newBlogPostDto, "Blog post created successfully"));

            // Act
            var result = await _controller.PostBlogPost(newBlogPostDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Result<BlogPostDTO>>(okResult.Value);
            Assert.True(returnValue.Succeeded);
            Assert.Equal("Blog post created successfully", returnValue.Message);
        }

        [Fact]
        public async Task Search_ReturnsOkResult_WithMatchingBlogPostDTOs()
        {
            // Arrange
            var mockBlogPosts = new List<BlogPostDTO>
        {
            new BlogPostDTO { Id = "1", Title = "Sample Post", Author = "Author 1" }
        };

            _mockBlogPostService.Setup(service => service.SearchAsync("Sample", null))
                                .ReturnsAsync(Result<IEnumerable<BlogPostDTO>>.Success(mockBlogPosts));

            // Act
            var result = await _controller.Search("Sample", null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Result<IEnumerable<BlogPostDTO>>>(okResult.Value);
            Assert.True(returnValue.Succeeded);
            Assert.Single(returnValue.Data);
        }

        [Fact]
        public async Task PostBlogPost_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var newBlogPostDto = new BlogPostDTO { Title = "", Author = "" }; // Invalid data
            _controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await _controller.PostBlogPost(newBlogPostDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<Result<BlogPostDTO>>(badRequestResult.Value);
            Assert.False(returnValue.Succeeded);
            Assert.Equal("Validation failed", returnValue.Message);
        }

        [Fact]
        public async Task GetBlogPost_ReturnsNotFound_WhenBlogPostDoesNotExist()
        {
            // Arrange
            _mockBlogPostService.Setup(service => service.GetByIdAsync(It.IsAny<string>()))
                                .ReturnsAsync(Result<BlogPostDTO>.Fail("Blog post not found"));

            // Act
            var result = await _controller.GetBlogPost("999"); // Non-existent ID

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<Result<BlogPostDTO>>(notFoundResult.Value);
            Assert.False(returnValue.Succeeded);
            Assert.Equal("Blog post not found", returnValue.Message);
        }
    }
}

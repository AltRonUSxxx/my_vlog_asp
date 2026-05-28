using Microsoft.AspNetCore.Mvc;
using my_vlog_asp.database.models;
using my_vlog_asp.DTOs;
using my_vlog_asp.Services;

namespace my_vlog_asp.Controllers.API
{
    [ApiController]
    [Route("/api/vlogs")]
    public class ApiController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserSevice;

        public ApiController(IProjectService speechLogService, ICurrentUserService currentUserSevice)
        {
            _projectService = speechLogService;
            _currentUserSevice = currentUserSevice;
        }

        [HttpGet]
        public ActionResult<List<PostDto>> GetAll()
        {
            var logs = _projectService.GetAllProjects();
            var res = logs.Select(log => ToDto(log));
            return Ok(res);
        }

        private static PostDto ToDto(PostView post)
        {
            return new PostDto
            {
                id = post.id,
                author = post.author_name,
                category = post.category,
                created_at = post.created_at,
                status = post.status,
                text = post.text,
                theme = post.theme
            };
        }

        [HttpPost]
        public IActionResult Create(PostCreateDto dto)
        {
            var userId = _currentUserSevice.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return Unauthorized(new { message = "Need to login in account" });
            }
            var newPost = new Post
            {
                category = dto.category,
                text = dto.text,
                theme = dto.theme,
                status = dto.status,
                created_at = DateTime.Now,
                author_id = userId.Value
            };
            _projectService.AddProject(newPost);
            return Ok(new { message = "Post created" });

        }
    }
}

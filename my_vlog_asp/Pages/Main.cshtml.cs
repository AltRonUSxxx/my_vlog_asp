using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using my_vlog_asp.database.models;
using my_vlog_asp.Services;

namespace my_vlog_asp.Pages
{
    public class HomeModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;

        [BindProperty]
        public string whatFind { get; set; } = string.Empty;
        public List<Post> Posts { get; set; } = new();
        public int TotalPostsCount = 0;

        public HomeModel(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        public void OnGet()
        {
            var user = _currentUserService.GetCurrentUser(HttpContext);
            if(user == null)
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/Index");
                return;
            }
            LoadProjects();
        }

        private void LoadProjects()
        {
            Posts = _projectService.GetAllProjects();
            TotalPostsCount = Posts.Count;
        }
    }
}

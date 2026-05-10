using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using my_vlog_asp.database.models;
using my_vlog_asp.Services;

namespace my_vlog_asp.Pages
{
    public class makePostModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        [BindProperty]
        public string theme { get; set; } = string.Empty;
        [BindProperty]
        public string text { get; set; } = string.Empty;
        [BindProperty]
        public string category { get; set; } = string.Empty;
        [BindProperty]
        public string Message { get; set; } = string.Empty;
        public makePostModel(IProjectService projectService, ICurrentUserService currentUserService)
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
        }

        public void OnPostMake()
        {
            if(string.IsNullOrEmpty(theme) && string.IsNullOrEmpty(text))
            {
                Message = "Fill fields";
                return;
            }

            string[] tags = category.Split(' ');
            foreach(string tag in tags)
            {
                if(!tag.StartsWith('#'))
                {
                    Message = "Use hashtag for category";
                    return;
                }
            }

            Post new_post = new Post();
            new_post.text = text;
            new_post.theme = theme;
            new_post.status = "OK";
            var user = _currentUserService.GetCurrentUser(HttpContext);
            new_post.author_id = user.id;
            new_post.category = string.Join(' ', tags);
            new_post.created_at = DateTime.Now;

            _projectService.AddProject(new_post);
            Message = "Success";
        }
    }
}

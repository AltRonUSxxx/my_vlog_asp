using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using my_vlog_asp.database.models;
using my_vlog_asp.Services;

namespace my_vlog_asp.Pages
{
    public class MeModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        public string username;
        public string login;
        public int age;
        private int _userId;

        public List<PostView> posts = new List<PostView>();
        public DateTime firstPost;

        public MeModel(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            _currentUserService.SignOut(HttpContext);
            return RedirectToPage();
        }

        private void onLoad(User user)
        {
            username = user.username;
            login = user.login;
            age = user.age;
            _userId = user.id;

            posts = _projectService.GetAllUserProjects(_userId);
            if (posts.Count > 0)
            {
                firstPost = posts[0].created_at;
            }
        }

        public IActionResult OnPostUnPost(int post_id)
        {
            try
            {
                Post post = _projectService.GetProjectById(post_id);
                if(post == null)
                {
                    return RedirectToPage();
                }
                bool isAuthor = _currentUserService.GetCurrentUser(HttpContext).id == post.author_id;
                if (isAuthor)
                {
                    _projectService.DeleteProject(post);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToPage();
        }

        public void OnGet()
        {
            var user = _currentUserService.GetCurrentUser(HttpContext);
            if (user == null)
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/Index");
                return;
            }
            onLoad(user);
        }
    }
}

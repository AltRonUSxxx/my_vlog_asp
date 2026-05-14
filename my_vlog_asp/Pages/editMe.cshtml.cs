using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using my_vlog_asp.database.models;
using my_vlog_asp.Services;

namespace my_vlog_asp.Pages
{
    public class editMeModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        private User _user;
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string login { get; set; }
        [BindProperty]
        public string old_password { get; set; }
        [BindProperty]
        public string new_password { get; set; }
        [BindProperty]
        public string confirm_password { get; set; }
        [BindProperty]
        public int age { get; set; }

        public string Message { get; set; }

        public editMeModel(IProjectService projectService, ICurrentUserService currentUserService)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        public IActionResult OnPostChangeMe()
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(login) || age <= 0)
            {
                Message = "Fill needable fields";
                return RedirectToPage();
            }
            if(new_password != confirm_password)
            {
                Message = "New password and confirm password should be same";
                return RedirectToPage();
            }
            User newUser = new User();
            newUser.username = username;
            newUser.login = login;
            newUser.age = age;

            return Redirect("/Me");
        }

        public IActionResult OnPostBack()
        {
            return Redirect("/Me");
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

        private void onLoad(User user)
        {
            username = user.username;
            login = user.login;
            age = user.age;
            _user = user;
        }

    }
}

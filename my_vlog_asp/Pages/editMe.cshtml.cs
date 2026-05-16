using Microsoft.AspNetCore.Identity;
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
        private PasswordHasher<User> _passwordHasher;
        public int userId { get; set; }
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
            _passwordHasher = new PasswordHasher<User>();
            _projectService = projectService;
            _currentUserService = currentUserService;
        }

        public void OnPostChangeMe(int user_id)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(login) || age <= 0)
            {
                Message = "Fill needable fields";
                return;
            }
            if(new_password != confirm_password)
            {
                Message = "New password and confirm password should be same";
                return;
            }
            if(new_password == null)
            {
                new_password = "";
            }
            User newUser = new User();
            newUser.username = username;
            newUser.login = login;
            newUser.age = age;

            string oldPasswordHash = _projectService.GetUserHashedPassword(user_id);

            var res = _passwordHasher.VerifyHashedPassword(
                newUser,
                oldPasswordHash,
                new_password
            );

            if (res != PasswordVerificationResult.Failed)
            {
                newUser.hashed_password = _passwordHasher.HashPassword(
                    newUser,
                    new_password
                );
            }
            else
            {
                newUser.hashed_password = null;
            }

            if (_projectService.UpdateUser(user_id, newUser))
            {
                Message = "Success";
                HttpContext.Response.Redirect("/Me");
            }
            else
            {
                Message = "Error";
            }

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
            userId = user.id;
        }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using my_vlog_asp.database;
using my_vlog_asp.database.models;
using my_vlog_asp.Services;

namespace my_vlog_asp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly app_db_context _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ICurrentUserService _crrentUserService;
        public IndexModel(app_db_context context, ICurrentUserService currentUserService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _crrentUserService = currentUserService;
        }

        [BindProperty]
        public string RegisterName { get; set; } = string.Empty;
        [BindProperty]
        public int? RegisterAge { get; set; }
        [BindProperty]
        public string RegisterLogin { get; set; } = string.Empty;
        [BindProperty]
        public string RegisterPassword { get; set; } = string.Empty;
        [BindProperty]
        public string RegistrRepeatPassword { get; set; } = string.Empty;

        [BindProperty]
        public string LoginLogin { get; set; } = string.Empty;
        [BindProperty]
        public string LoginPassword { get; set; } = string.Empty;

        public bool IsAuthorized { get; set; }
        public string CurrentUserName { get; set; } = string.Empty;
        public string CurrentUserLogin { get; set; } = string.Empty;
        public int CurrentUserAge { get; set; }
        public string Message { get; set; } = string.Empty;
        public void OnGet()
        {
            LoadUser();
        }

        public IActionResult OnPostRegister()
        {
            LoadUser();
            if (string.IsNullOrEmpty(RegisterName)
                || string.IsNullOrEmpty(RegisterLogin)
                || string.IsNullOrEmpty(RegisterPassword)
                || RegisterAge == null)
            {
                Message = "Fill all fields";
                return Page();
            }

            if (RegisterAge <= 0)
            {
                Message = "Only positive age";
                return Page();
            }

            if (_context.Users.Any(u => u.login == RegisterLogin))
            {
                Message = "This login already taken";
                return Page();
            }
            if (RegisterPassword != RegistrRepeatPassword || string.IsNullOrEmpty(RegistrRepeatPassword))
            {
                Message = "Passwords should be same";
                return Page();
            }
            var user = new User
            {
                username = RegisterName,
                login = RegisterLogin,
                age = RegisterAge.Value,
                role_id = 1
            };

            user.hashed_password = _passwordHasher.HashPassword(new User(), RegisterPassword);

            _context.Users.Add(user);
            _context.SaveChanges();

            //HttpContext.Session.SetInt32("UserId", user.Id);
            _crrentUserService.SignIn(HttpContext, user.id);

            return RedirectToPage();
        }
        public IActionResult OnPostLogin()
        {
            LoadUser();
            if (string.IsNullOrEmpty(LoginLogin) || string.IsNullOrEmpty(LoginPassword))
            {
                Message = "Enter login and password";
                return Page();
            }
            var user = _context.Users.FirstOrDefault(u => u.login == LoginLogin);

            if (user == null)
            {
                Message = "Uncorrect login or password";
                return Page();
            }
            var res = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.hashed_password,
                    LoginPassword
                );

            if (res == PasswordVerificationResult.Failed)
            {
                Message = "Uncorrect login or password";
                return Page();
            }
            //HttpContext.Session.SetInt32("UserId", user.Id);
            _crrentUserService.SignIn(HttpContext, user.id);

            return RedirectToPage();
        }
        public IActionResult OnPostLogout()
        {
            //HttpContext.Session.Clear();
            _crrentUserService.SignOut(HttpContext);
            return RedirectToPage();
        }
        private void LoadUser()
        {
            //var userId = HttpContext.Session.GetInt32("UserId");
            //if(userId == null) 
            //{
            //    IsAuthorized = false;
            //    return;
            //}

            var user = _crrentUserService.GetCurrentUser(HttpContext);

            if (user == null)
            {
                IsAuthorized = false;
                HttpContext.Session.Clear();
                return;
            }
            IsAuthorized = true;
            CurrentUserName = user.username;
            CurrentUserLogin = user.login;
            CurrentUserAge = user.age;

        }
    }
}

using my_vlog_asp.database.models;
using my_vlog_asp.Services;
using my_vlog_asp.database;

namespace my_vlog_asp.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly app_db_context _context;
        public CurrentUserService(app_db_context context)
        {
            _context = context;
        }
        public bool IsAuthenticated(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("UserId") != null;
        }
        public int? GetCurrentUserId(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("UserId");
        }
        public User? GetCurrentUser(HttpContext httpContext)
        {
            var userId = GetCurrentUserId(httpContext);

            if (userId == null)
            {
                return null;
            }

            return _context.Users.FirstOrDefault(u => u.id == userId.Value);
        }

        public void SignIn(HttpContext httpContext, int userId)
        {
            httpContext.Session.SetInt32("UserId", userId);
        }

        public void SignOut(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }
    }
}

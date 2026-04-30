using my_vlog_asp.database.models;

namespace my_vlog_asp.Services
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated(HttpContext httpContext);
        int? GetCurrentUserId(HttpContext httpContext);
        User? GetCurrentUser(HttpContext httpContext);
        void SignIn(HttpContext httpContext, int userId);
        void SignOut(HttpContext httpContext);
    }
}


using my_vlog_asp.database.models;

namespace my_vlog_asp.Services
{
    public interface IProjectService
    {
        List<PostView> GetAllProjects();
        List<PostView> GetAllUserProjects(int authorId);
        List<Post> GetProjectByAuthorId(int authorId);
        Post? GetProjectById(int auhorId);
        void AddProject(Post project);
        void UpdateProject(Post project);
        void DeleteProject(Post project);
        string GetUserHashedPassword(int userId);
        bool UpdateUser(int user_id, User newUser);
        bool ProjectExists(int id);
    }
}

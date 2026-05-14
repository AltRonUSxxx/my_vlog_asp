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
        public bool UpdateUser(User newUser, string OldPassword, string newPassword);
        bool ProjectExists(int id);
    }
}

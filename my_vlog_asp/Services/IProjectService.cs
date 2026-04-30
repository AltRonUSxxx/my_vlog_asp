using my_vlog_asp.database.models;

namespace my_vlog_asp.Services
{
    public interface IProjectService
    {
        List<Post> GetAllProjects();
        List<Post> GetProjectByAuthorId(int authorId);
        Post? GetProjectById(int auhorId);
        void AddProject(Post project);
        void UpdateProject(Post project);
        void DeleteProject(Post project);

        bool ProjectExists(int id);
    }
}

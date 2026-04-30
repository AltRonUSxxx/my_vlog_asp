using Microsoft.EntityFrameworkCore;
using my_vlog_asp.database;
using my_vlog_asp.database.models;
using my_vlog_asp.Services;

namespace my_vlog_asp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly app_db_context _context;
        public ProjectService(app_db_context context) 
        {
            _context = context;
        }
        public List<Post> GetAllProjects() 
        {
            return _context.Posts
                .OrderByDescending(p => p.created_at)
                .ThenByDescending(p => p.id)
                .ToList();
        }
        public List<Post> GetProjectByAuthorId(int authorId)
        {
            return _context.Posts
                .Where(p => p.author_id == authorId)
                .OrderByDescending(p => p.created_at)
                .ThenByDescending(p => p.id)
                .ToList();
        }
        public Post? GetProjectById(int id) 
        {
            return _context.Posts
                .FirstOrDefault(p => p.id == id);
        }
        public void AddProject(Post project)
        {
            _context.Posts.Add(project);
            _context.SaveChanges();
        }
        public void UpdateProject(Post project) 
        {
            _context.Posts.Update(project);
            _context.SaveChanges();
        }
        public void DeleteProject(Post project)
        {
            _context.Posts.Remove(project);
            _context.SaveChanges();
        }

        public bool ProjectExists(int id)
        {
            return (_context.Posts.Any(p => p.id == id));
        }
    }
}

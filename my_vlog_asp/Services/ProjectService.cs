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
        public List<PostView> GetAllProjects() 
        {
            return PostsToPostViews(_context.Posts
                .Where(x => x.is_deleted == false)
                .OrderByDescending(p => p.created_at)
                .ThenByDescending(p => p.id)
                .ToList());
        }

        public string GetUserHashedPassword(int user_id)
        {
            User user = _context.Users.FirstOrDefault(x => x.id == user_id);
            if(user != null)
            {
                return user.hashed_password;
            }
            return "null";
        }

        public bool UpdateUser(int user_id, User newUser)
        {
            User oldUser = _context.Users.FirstOrDefault(x => x.id == user_id);
            if(oldUser == null)
            {
                return false;
            }

            try
            {
                if(newUser.hashed_password != null)
                {
                    oldUser.hashed_password = newUser.hashed_password;
                }
                oldUser.username = newUser.username;
                oldUser.role_id = newUser.role_id;
                oldUser.age = newUser.age;
                oldUser.login = newUser.login;
                _context.Users.Update(oldUser);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PostView> GetAllUserProjects(int authorId)
        {
            return PostsToPostViews(_context.Posts
                .Where(x => x.is_deleted == false && x.author_id == authorId)
                .OrderByDescending(p => p.created_at)
                .ThenByDescending(p => p.id)
                .ToList());
        }

        private List<PostView> PostsToPostViews(List<Post> posts)
        {
            List<PostView> newPosts = new List<PostView>();
            foreach(Post post in posts)
            {
                newPosts.Add(PostToPostView(post));
            }
            return newPosts;
        }

        private PostView PostToPostView(Post post)
        {
            return new PostView(post, _context.Users.FirstOrDefault(x => x.id == post.author_id).login);
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
            project.is_deleted = true;
            _context.Posts.Update(project);
            _context.SaveChanges();
        }

        public bool ProjectExists(int id)
        {
            return (_context.Posts.Any(p => p.id == id));
        }
    }
}

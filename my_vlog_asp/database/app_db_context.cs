using Microsoft.EntityFrameworkCore;
using my_vlog_asp.database.models;
namespace my_vlog_asp.database
{
    public class app_db_context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public app_db_context(DbContextOptions<app_db_context> options) : base(options)
        {

        }
    }
}

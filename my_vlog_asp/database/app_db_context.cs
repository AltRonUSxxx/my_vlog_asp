using Microsoft.EntityFrameworkCore;
using my_vlog_asp.database.models;
using my_vlog_asp.Migrations;
using System.Data;
namespace my_vlog_asp.database
{
    public class app_db_context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public app_db_context(DbContextOptions<app_db_context> options) : base(options)
        {

        }

        public static void addroles(app_db_context context)
        {
            Role new_role = new Role();
            new_role.role = "USER";
            new_role.id = 1;
            context.Roles.Add(new_role);
        }
    }
}

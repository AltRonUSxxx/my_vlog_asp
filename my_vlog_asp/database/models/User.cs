namespace my_vlog_asp.database.models
{
    public class User
    {
        public int id {  get; set; }
        public string username {  get; set; }
        public string login {  get; set; }
        public string hashed_password {  get; set; }
        public int role_id {  get; set; }
        public int age {  get; set; }
        public Role role { get; set; }
        public List<Post> posts{ get; set; }
    }
}

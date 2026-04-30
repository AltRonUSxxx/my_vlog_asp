namespace my_vlog_asp.database.models
{
    public class Post
    {
        public int id { get; set; }
        public int author_id { get; set; }
        public string category { get; set; }
        public DateTime created_at { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public bool is_deleted { get; set; }
    }
}

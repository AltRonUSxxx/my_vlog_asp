namespace my_vlog_asp.database.models
{
    public class PostView
    {
        public int id { get; set; }
        public string author_name { get; set; }

        public string category { get; set; }
        public DateTime created_at { get; set; }
        public string text { get; set; }

        public string theme { get; set; }
        public string status { get; set; }

        public PostView(Post post, string author_name)
        {
            this.id = post.id;
            this.author_name = author_name;
            this.category = post.category;
            this.created_at = post.created_at;
            this.text = post.text;
            this.theme = post.theme;
            this.status = post.status;
        }
    }

}

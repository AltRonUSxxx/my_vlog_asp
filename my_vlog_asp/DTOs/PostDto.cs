namespace my_vlog_asp.DTOs
{
    public class PostDto
    {
        public int id { get; set; }
        public string author { get; set; }

        public string category { get; set; }
        public DateTime created_at { get; set; }
        public string text { get; set; }

        public string theme { get; set; }
        public string status { get; set; }
        public bool is_deleted { get; set; }
    }
}

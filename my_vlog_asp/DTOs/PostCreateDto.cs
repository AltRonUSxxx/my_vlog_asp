using System.ComponentModel.DataAnnotations;
namespace my_vlog_asp.DTOs
{
    public class PostCreateDto
    {
        [Required]

        public string category { get; set; }
        public string text { get; set; }

        public string theme { get; set; }
        public string status { get; set; }
    }
}

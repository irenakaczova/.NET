
namespace InstagramWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public int? Likes { get; set; } = 0;
        public string? Text { get; set; }

        public int PostId { get; set; }
        public virtual Post? Post { get; set; } 
        public Comment() { }
    }
}
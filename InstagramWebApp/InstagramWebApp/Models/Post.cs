
namespace InstagramWebApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? ImgPath { get; set; }
        public string Tags { get; set; } = string.Empty;
        public int? Likes { get; set; } = 0;
        public DateTime ReleaseDate { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
        public Post() { }
    }
}
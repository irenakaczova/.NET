
namespace Instagram
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string ImgPath { get; set; }
        public string Tags { get; set; } = string.Empty;
        public int Likes { get; set; } = 0;
        public DateTime ReleaseDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }
        public Post() { }
    }
}
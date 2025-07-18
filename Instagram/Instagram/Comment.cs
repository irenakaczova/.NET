
namespace Instagram
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int Likes { get; set; } = 0;

        public int PostId { get; set; }
        public virtual Post Post { get; set; } = null!;
        public Comment() { }
    }
}
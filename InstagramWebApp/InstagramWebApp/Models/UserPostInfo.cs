
namespace InstagramWebApp.Models
{
    public class UserPostInfo
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int PostId { get; set; }
        public virtual Post Post { get; set; } = null!;

        public bool? Liked { get; set; }

        public UserPostInfo() { }
    }
}
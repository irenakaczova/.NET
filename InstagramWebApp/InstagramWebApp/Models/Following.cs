namespace InstagramWebApp.Models
{
    public class Following
    {
        public string FollowerUserId { get; set; }
        public virtual User FollowerUser { get; set; }

        public string FollowedUserId { get; set; }
        public virtual User FollowedUser { get; set; }

        public Following() { }
    }
}
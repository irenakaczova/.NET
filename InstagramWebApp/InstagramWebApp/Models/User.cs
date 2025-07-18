using Microsoft.AspNetCore.Identity; 

namespace InstagramWebApp.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Bio { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Following>? Following { get; set; }
        public virtual ICollection<Following>? Followers { get; set; }

        public User() { }
    }
}
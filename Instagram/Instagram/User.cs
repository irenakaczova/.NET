﻿
namespace Instagram
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public User() { }
    }
}
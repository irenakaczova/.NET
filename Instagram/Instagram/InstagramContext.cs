using Microsoft.EntityFrameworkCore;

namespace Instagram
{
    public class InstagramContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserPostInfo> UserPostInfos { get; set; }
        public DbSet<Message> Messages { get; set; }

        public InstagramContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies().UseSqlite(@"Data Source=ig.db");
        }
        
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne().HasForeignKey("UserId")
            .IsRequired();

            modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne().HasForeignKey("PostId")
            .IsRequired();
        }
        */
    }
}
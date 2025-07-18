using Instagram;

var ctx = new InstagramContext();

// dummy data
ctx.Users.Add(new User { FirstName = "John", LastName = "Doe", Age = 30 });
ctx.Users.Add(new User { FirstName = "Alice", LastName = "Smith", Age = 25 });
ctx.Users.Add(new User { FirstName = "Bob", LastName = "Johnson", Age = 28 });

var userJohn = ctx.Users.FirstOrDefault(u => u.FirstName == "John");
var userAlice = ctx.Users.FirstOrDefault(u => u.FirstName == "Alice");

ctx.Posts.Add(new Post { Text = "First post by John", Likes = 10, ReleaseDate = DateTime.Now.AddDays(-5), User = userJohn });
ctx.Posts.Add(new Post { Text = "Another post by John", Likes = 15, ReleaseDate = DateTime.Now.AddDays(-10), User = userJohn });
ctx.Posts.Add(new Post { Text = "Post by Alice", Likes = 8, ReleaseDate = DateTime.Now.AddDays(-3), User = userAlice });

var postByJohn = ctx.Posts.Where(p => p.Text == "First post by John").First();
ctx.Comments.Add(new Comment { Author = "Commenter1", Likes = 5, Post = postByJohn });
ctx.Comments.Add(new Comment { Author = "Commenter2", Likes = 3, Post = postByJohn });

ctx.UserPostInfos.Add(new UserPostInfo { User = userAlice, Post = postByJohn, Liked = true });

ctx.SaveChanges();


var userId = 1;

var olderThanAWeekPosts = ctx.Posts
        .Where(p => p.UserId == userId && p.ReleaseDate < DateTime.Now.AddDays(-7) && !p.Text.ToLower().Contains("já"))
        .ToList();

var likedPosts = ctx.UserPostInfos
        .Where(upi => upi.UserId == userId && upi.Liked)
        .Select(upi => upi.Post)
        .ToList();

var userId1 = 1;
var userId2 = 2;

var bothLikedPosts = ctx.UserPostInfos
        .GroupBy(upi => upi.PostId)
        .Where(g => g.Count() >= 2 && g.Any(x => x.UserId == userId1) && g.Any(x => x.UserId == userId2))
        .SelectMany(g => g.Select(x => x.Post))
        .ToList();


Console.WriteLine("Older than a week posts:");
foreach (var post in olderThanAWeekPosts)
{
    Console.WriteLine($"ID: {post.Id}, Text: {post.Text}, Date: {post.ReleaseDate}");
}

Console.WriteLine("\nLiked posts:");
foreach (var post in likedPosts)
{
    Console.WriteLine($"ID: {post.Id}, Text: {post.Text}, Likes: {post.Likes}");
}

Console.WriteLine("\nPosts liked by both users:");
foreach (var post in bothLikedPosts)
{
    Console.WriteLine($"ID: {post.Id}, Text: {post.Text}, Datum vydání: {post.ReleaseDate}");
}
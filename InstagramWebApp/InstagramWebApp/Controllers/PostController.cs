using InstagramWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstagramWebApp.Data;
using Microsoft.AspNetCore.Identity;

namespace InstagramWebApp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public PostController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                if (currentUser != null)
                {
                    post.UserId = currentUser.Id;
                    post.User = currentUser;

                    _context.Posts.Add(post);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(post);
        }

        public IActionResult Edit(int id)
        {
            var post = GetPostById(id);

            if (!UserOwnsPost(post))
            {
                return Forbid();
            }

            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                if (!UserOwnsPost(post))
                {
                    return Forbid();
                }

                _context.Posts.Update(post);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        public IActionResult Delete(int id)
        {
            var post = GetPostById(id);
            if (post != null)
            {
                if (!UserOwnsPost(post))
                {
                    return Forbid();
                }

                _context.Posts.Remove(post);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            return NotFound();
        }

        public IActionResult Comments(int postId)
        {
            var post = GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            var comments = _context.Comments
                .Where(c => c.PostId == postId)
                .ToList();

            post.Comments = comments;

            return View(post);
        }

        [HttpGet]
        public IActionResult AddComment(int postId)
        {
            var post = _context.Posts.Find(postId);

            if (post == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                PostId = postId,
                Post = post
            };

            ViewData["Post"] = post;
            return View(comment);
        }


        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var post = _context.Posts.Find(comment.PostId);

                if (post == null)
                {
                    return NotFound();
                }

                comment.Post = post;

                _context.Comments.Add(comment);
                _context.SaveChanges();

                return RedirectToAction("Comments", new { postId = comment.PostId });
            }

            return View(comment);
        }

        private Post? GetPostById(int id) => _context.Posts.FirstOrDefault(p => p.Id == id);

        private bool UserOwnsPost(Post? post)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            return currentUser != null && post != null && post.UserId == currentUser.Id;
        }
    }
}
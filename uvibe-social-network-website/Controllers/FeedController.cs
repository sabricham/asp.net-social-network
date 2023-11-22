using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uvibe_social_network_website.Data;

namespace uvibe_social_network_website.Controllers
{
    public class FeedController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string controllerName = "Feed";

        public FeedController(ApplicationDbContext db)
        {
            _db = db;
        }

        //main page full of posts
        public IActionResult Index()
        {
            /*IEnumerable<Post> objPostList = _db.Posts;
            return View(objPostList);*/

            //Dynamic view of the website
            ViewBag.controllerName = controllerName;
            return View();
        }
    }
}

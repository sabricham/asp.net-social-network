using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using uvibe_social_network_website.Data;
using uvibe_social_network_website.Models;

namespace uvibe_social_network_website.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string controllerName = "Profile";

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        { 
            ViewBag.ControllerName = controllerName;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            //check if the user email matches the user email from database
            if (!_db.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "The user Email does not exist");
                return View();
            }

            User userFromDatabase = _db.Users.Where(u => u.Email == user.Email).First();

            //check if the user password matches the user password from database
            if(user.Password != userFromDatabase.Password)
            {
                ModelState.AddModelError("Email", "The Email might be incorrect");
                ModelState.AddModelError("Password", "The Password might be incorrect");
                return View();
            }

            //create the cookie
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userFromDatabase.Nickname),
                new Claim(ClaimTypes.Email, userFromDatabase.Email),
                new Claim(ClaimTypes.NameIdentifier, userFromDatabase.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
           
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

            return RedirectToAction("Index", "Feed");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Feed");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            //check if there is already a user with the selected nickname
            if(_db.Users.Any(u => u.Nickname == user.Nickname))
                ModelState.AddModelError("Nickname", "The Nickname is already taken");

            //check if there is already a user with the selected email
            if (_db.Users.Any(u => u.Email == user.Email))
                ModelState.AddModelError("Email", "The Email is already taken");

            //if nothing wrong create the user in the database
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}

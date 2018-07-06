using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Etkinlik.Models;

namespace Etkinlik.Controllers
{
    [Route("user")]
    public class HomeController : Controller
    {
        #region ProtectedMember
        protected ApplicationDbContext mContext;

        #endregion
        public HomeController(ApplicationDbContext context)
        {
            mContext = context;
        }
        /*public IActionResult Index()
        {
            //create db
            mContext.Database.EnsureCreated();

            mContext.SaveChanges();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password) {
            ViewData["username"] = username;
            ViewData["passowrd"] = password;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/


        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            //create db
            mContext.Database.EnsureCreated();

            return View();
        }

       /* [HttpGet]
        [Route("signup")]
        public IActionResult Signup()
        {
            return View("SignUp", new UserModel());
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult Signup(UserModel user)
        {
            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            mContext.Users.Add(user);
            mContext.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(UserModel user)
        {
            return RedirectToAction("login");
        }*/
    }
}

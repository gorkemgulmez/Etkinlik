using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Etkinlik.Models;

namespace Etkinlik.Controllers
{
    public class HomeController : Controller
    {
        #region ProtectedMember
        protected ApplicationDbContext mContext;

        #endregion
        public HomeController(ApplicationDbContext context)
        {
            mContext = context;
        }
        public IActionResult Index()
        {

            //create db
            mContext.Database.EnsureCreated();

            if (!mContext.Users.Any())
            {

                mContext.Users.Add(new UserModel
                {
                    userID = 1,
                    userName = "gorkemgulmez",
                    fullName = "Görkem Gülmez",
                    userEmail = "gorkemgulmez@outlook.com",
                    Password = "1234"
                });
            }
            mContext.SaveChanges();

            if (!mContext.Posts.Any())
            {
                mContext.Posts.Add(new PostModel
                {
                    postID = 1,
                    postName = "Etkinlik",
                    postDesc = "Piknik",
                    UserModel = mContext.Users.First()
                });
            }
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
        }
    }
}

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
                    UserName = "gorkemgulmez",
                    FullName = "Görkem Gülmez",
                    UserEmail = "gorkemgulmez@outlook.com",
                    Password = "12366664"
                });
            }
            mContext.SaveChanges();

            if (!mContext.Posts.Any())
            {
                mContext.Posts.Add(new PostModel
                {
                    PostName = "Etkinlik",
                    PostDesc = "Piknik",
                    UserModelId = 1
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

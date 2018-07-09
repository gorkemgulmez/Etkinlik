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

        [Route("")]
        [Route("Index")]
        [Route("~/")]
        public IActionResult Index()
        {
            //create db
            mContext.Database.EnsureCreated();

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
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
            if( user.UserName !=null && user.UserName.Length >= 5 && user.UserName.Length <= 25
                && user.Password.Length >= 5 && user.Password.Length <= 25
                && user.FullName.Length >= 5 && user.FullName.Length <= 25
                && user.UserEmail.Length >= 5 && user.UserEmail.Length <= 150)
            {
                mContext.Users.Add(user);
                mContext.SaveChanges();
                return RedirectToAction("Login");
            }

            return RedirectToAction("Signup");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserModel user)
        {
            var lUser = mContext.Users.Where(usr => usr.UserName == user.UserName).FirstOrDefault();
            if(lUser != null && lUser.Password == user.Password)
                return View("Index");
            //TODO hata mesajıyla gönder
            //hatalı kullanıcı adı ve ya şifre
            return RedirectToAction("Login");
            
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login", new UserModel());
        }
    }
}

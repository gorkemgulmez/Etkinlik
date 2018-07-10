using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Etkinlik.Models;
using Microsoft.AspNetCore.Identity;

namespace Etkinlik.Controllers
{

    [Route("user")]
    public class HomeController : Controller
    {
        #region ProtectedMember
        protected ApplicationDbContext mContext;
        protected UserManager<UserModel> mUserManager;
        protected SignInManager<UserModel> mSignInManager;
        #endregion

        public HomeController(ApplicationDbContext context,
                              UserManager<UserModel> userManager,
                              SignInManager<UserModel> signInManager)
        {
            mContext = context;
            mSignInManager = signInManager;
            mUserManager = userManager;
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

        /*[HttpGet]
        [Route("signup")]
        public IActionResult Signup()
        {
            return View("SignUp", new UserModel());
        }*/

        //[HttpPost]
        [Route("signup")]
        public async Task<IActionResult> CreateUserAsync(/*UserModel user*/)
        {

            var result = await mUserManager.CreateAsync(new UserModel
            {
                UserName = "gooorkemgulmez",
                Email = "sdsfdf@dsf.co",
                FullName = "Goöörkem Gulmez"
            }, "passwordAS1123*");
            if(result.Succeeded)
                return Content("Success");

            return Content("Failed!!");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserModel user)
        {
           /* var lUser = mContext.Users.Where(usr => usr.UserName == user.UserName).FirstOrDefault();
            if(lUser != null && lUser.Password == user.Password)
                return View("Index");

            ViewData["Message"] = "Hatalı Kullanıcı adı veya şifre";*/
            return View("Login");
            
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login", new UserModel());
        }
    }
}

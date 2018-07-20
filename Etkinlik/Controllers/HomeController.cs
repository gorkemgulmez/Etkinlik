using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Etkinlik.Models;
using Etkinlik.Data;
using Microsoft.AspNetCore.Identity;

namespace Etkinlik.Controllers
{
    public class HomeController : Controller
    {
        #region private-readonly
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion
        //constructor
        public HomeController(ApplicationDbContext context,
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _applicationDbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var allPosts = _applicationDbContext.Posts.ToList();

            return View(allPosts);
        }

        public IActionResult AddActivity()
        {
            return Redirect("/Post/add");
        }

        public IActionResult AddSurvey()
        {
            return Redirect("/Survey/add");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool HasIn(PostModel post) {
            ApplicationUser user;
            try {
                if (!_signInManager.IsSignedIn(User))
                    return false;
                user = _applicationDbContext.Users.First(u => u.Id == _userManager.GetUserId(User));
                UserPostModel userPost = _applicationDbContext.UserPosts.First(d =>
                         d.ApplicationUserId == user.Id && d.PostModelId == post.Id);
            }catch(Exception){
                return false;
            }
            return true;
        }
    }
}

/*
@{
    ViewData["Title"] = "Contact";
}
<h2>@ViewData["Title"]</h2>
<h3>@ViewData["Message"]</h3>


ViewData["Message"] = "Your application description page."; 
return View();
*/

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

            return View("EtkinlikAnaSayfa",allPosts);
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
     
        [HttpGet("/test")]
        public IActionResult test() 
        {
            var surveyList = _applicationDbContext.Surveys.Where(s=> true).ToList();
            foreach(var survey in surveyList)
            {
                survey.SurveyChoiceModel = _applicationDbContext.SurveyChoices.Where(sc => sc.SurveyModelId == survey.Id).ToList();
                
            }
            
            return View("PostDetail", surveyList);
        }

        [HttpGet("getLastSurvey")]
        public IActionResult getLastSurvey()
        {
            var item = _applicationDbContext.Surveys.Last(e => true);
            item.SurveyChoiceModel = null;
            _applicationDbContext.Update(item);
            _applicationDbContext.SaveChanges();
            var list = _applicationDbContext.SurveyChoices.Where
                (sc => sc.SurveyModelId == item.Id).ToList();
            return Json(list);
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

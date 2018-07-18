using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Etkinlik.Models;
using Etkinlik.Data;

namespace Etkinlik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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

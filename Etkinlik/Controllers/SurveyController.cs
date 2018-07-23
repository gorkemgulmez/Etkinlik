using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etkinlik.Data;
using Etkinlik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.Controllers
{
    [Route("Survey")]
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SurveyController(ApplicationDbContext applicationDbContext,
                            UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var surveyList = _applicationDbContext.Surveys.Where(
                s=> s.ApplicationUserId == _userManager.GetUserId(HttpContext.User) ).ToList();

            return View("Index", surveyList);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddSurvey()
        {
            return View("AddSurvey", new SurveyModel());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddSurvey(SurveyModel surveyModel)
        {
            if (surveyModel == null)
                return Redirect("/");

            if (surveyModel.SurveyTitle == null)
                return Redirect("/");

            surveyModel.ApplicationUserId = _userManager.GetUserId(HttpContext.User);
            _applicationDbContext.Surveys.Add(surveyModel);
            _applicationDbContext.SaveChanges();
            return Redirect("/Survey");
        }

        [HttpGet("delete/{id}")]
        public IActionResult DeleteSurvey(int id)
        {
            ApplicationUser user = _applicationDbContext.Users.First(u => u.Id == _userManager.GetUserId(HttpContext.User));
            SurveyModel delSurvey;
            try
            {
                delSurvey = _applicationDbContext.Surveys.First(p => p.Id == id);
            }
            catch (Exception)
            {
                return Redirect("/");
            }
            if (delSurvey == null)
                return Redirect("/");

            try
            {
                if (delSurvey.ApplicationUserId != user.Id)
                    return Redirect("/");
            }
            catch (Exception)
            {
                return Redirect("/");
            }
            _applicationDbContext.Remove(delSurvey);
            _applicationDbContext.SaveChanges();
            return Redirect("/Survey");
        }
        
        public IActionResult JoinSurvey(SurveyChoiceModel selection)
        {
            selection.Vote += 1;
            return View();
        }

        public List<SurveyChoiceModel> getChoices(SurveyModel survey)
        {
            List<SurveyChoiceModel> choices = _applicationDbContext.SurveyChoices.Where(sc => sc.SurveyModelId == survey.Id).ToList();
            return choices;
        }

    }
}

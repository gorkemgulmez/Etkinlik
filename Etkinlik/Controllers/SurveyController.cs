using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etkinlik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.Controllers
{
    [Route("Survey")]
    public class SurveyController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetSurvey()
        {
            return View();
        }

        [HttpGet]
        [Route("add")]
        [Authorize]
        public IActionResult AddSurvey()
        {
            return View("AddSurvey", new SurveyModel());
        }

        [HttpPost]
        [Route("add")]
        [Authorize]
        public IActionResult AddSurvey(SurveyModel surveyModel)
        {
            if (surveyModel == null)
                return View();

            if (surveyModel.Title == null)
                return View();
            return View();
        }

        public IActionResult DeleteSurvey(SurveyModel survey)
        {
            return View();
        }

        [Authorize]
        public IActionResult JoinSurvey(AnswerModel answer)
        {
            answer.Vote += 1;
            return View();
        }
    }
}

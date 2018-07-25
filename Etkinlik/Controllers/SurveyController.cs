﻿using System;
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
                s => s.ApplicationUserId == _userManager.GetUserId(HttpContext.User)).ToList();

            return View("Index", surveyList);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult AddSurvey()
        {
            return View("AddSurvey", new SurveyModel());
        }

        [HttpGet("update/{id}")]
        [Authorize]
        public IActionResult UpdateSurvey(int id)
        {
            SurveyModel sModel;
            try
            {
                sModel = _applicationDbContext.Surveys.First(s => s.Id == id);
            }
            catch (Exception)
            {
                return Redirect("/");
            }

            if (!sModel.ApplicationUserId.Equals(_userManager.GetUserId(HttpContext.User)))
                return Redirect("/");

            sModel.SurveyChoiceModel = _applicationDbContext.SurveyChoices.Where(sc => sc.SurveyModelId == sModel.Id).ToList();

            SurveyModel updateActivityModel = new SurveyModel
            {
                Id = sModel.Id,
                SurveyTitle = sModel.SurveyTitle,
                SurveyChoiceModel = sModel.SurveyChoiceModel
            };

            return View("UpdateSurvey", updateActivityModel);
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

        [Authorize]
        [HttpPost("update/{id}")]
        public IActionResult UpdateSurvey(int id, SurveyModel survey)
        {
            SurveyModel updSurvey = _applicationDbContext.Surveys.First(s => s.Id == id);
            if (updSurvey == null)
                return Redirect("/");

            if (!_userManager.GetUserId(User).Equals(updSurvey.ApplicationUserId))
                return Redirect("/");

            if (!survey.SurveyTitle.Equals("") && !survey.SurveyTitle.Equals(updSurvey.SurveyTitle))
                updSurvey.SurveyTitle = survey.SurveyTitle;
            if (!survey.SurveyChoiceModel.Equals(updSurvey.SurveyChoiceModel))
                updSurvey.SurveyChoiceModel = survey.SurveyChoiceModel;

            _applicationDbContext.Surveys.Update(updSurvey);
            _applicationDbContext.SaveChanges();
            return Redirect("/");
        }

        [Authorize]
        [HttpGet("/vote/{id}")]
        public IActionResult JoinSurvey(int id)
        {
            UserSurveyModel userVote;
            var userId = _userManager.GetUserId(HttpContext.User);
            try
            {
                userVote = _applicationDbContext.UserSurveys.First(us => us.SurveyChoiceModelId == id && us.ApplicationUserId == userId);
            }
            catch(Exception)
            {
                SurveyChoiceModel option = _applicationDbContext.SurveyChoices.First(o => o.Id == id);
                List<SurveyChoiceModel> modelList = _applicationDbContext.SurveyChoices.Where(sc => sc).ToList();
                foreach(var model in modelList) { 
                    if (_applicationDbContext.UserSurveys.Any(us => us.SurveyChoiceModelId == model.Id && us.ApplicationUserId == userId))
                    {
                        var delVote = _applicationDbContext.UserSurveys.First(us => us.SurveyChoiceModelId == model.Id && us.ApplicationUserId == userId);
                        _applicationDbContext.UserSurveys.Remove(delVote);
                        model.Vote -= 1;
                    }
                }
                _applicationDbContext.UserSurveys.Add(new UserSurveyModel
                {
                    ApplicationUserId = userId,
                    SurveyChoiceModelId = option.Id
                });
                option.Vote += 1;
                _applicationDbContext.SaveChanges();
                return Redirect("/");
            }

            _applicationDbContext.SurveyChoices.First(sc => sc.Id == id).Vote -= 1; 
            _applicationDbContext.UserSurveys.Remove(userVote);
            _applicationDbContext.SaveChanges();
            return Redirect("/");
        }

        public List<SurveyChoiceModel> getChoices(SurveyModel survey)
        {
            List<SurveyChoiceModel> choices = _applicationDbContext.SurveyChoices.Where(sc => sc.SurveyModelId == survey.Id).ToList();
            return choices;
        }

    }
}

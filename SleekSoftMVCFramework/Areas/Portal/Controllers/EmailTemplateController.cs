﻿using log4net;
using SleekSoftMVCFramework.Controllers;
using SleekSoftMVCFramework.Data.Entities;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Repository.CoreRepositories;
using SleekSoftMVCFramework.Utilities;
using SleekSoftMVCFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SleekSoftMVCFramework.Areas.APPPortal.Controllers
{
    [Authorize]
    public class EmailTemplateController : BaseController
    {

        private readonly IRepositoryQuery<EmailTemplate,long> _EmailTemplateQuery;
        private readonly IRepositoryQuery<EmailToken, long> _EmailTokenQuery;
        private readonly IRepositoryCommand<EmailTemplate, long> _EmailTemplateCommand;
        private readonly IActivityLogRepositoryCommand _activityRepo;
        private readonly ILog _log;
        private readonly Utility _utility;

        public EmailTemplateController(IActivityLogRepositoryCommand activityRepo, IRepositoryQuery<EmailToken, long> EmailTokenQuery, IRepositoryQuery<EmailTemplate, long> EmailTemplateQuery, Utility utility, IRepositoryCommand<EmailTemplate, long> EmailTemplateCommand, ILog log)
        {

            _EmailTemplateQuery = EmailTemplateQuery;
            _EmailTemplateCommand = EmailTemplateCommand;
            _EmailTokenQuery = EmailTokenQuery;
            _activityRepo = activityRepo;
            _utility = utility;
            _log = log;
        }
        // GET: APPPortal/EmailTemplate

        public ActionResult Index()
        {
            try
            {
                if (TempData["MESSAGE"] != null)
                {
                    ViewBag.Msg = TempData["MESSAGE"] as string;
                }
                var model = _EmailTemplateQuery.GetAll().Select(e => new EmailViewModel()
                {
                    EmailID = e.Id,
                    EmailSubject = e.Name,
                    EmailCode = e.Code

                });
                return View(model);
            }
            catch (Exception ex)
            {
                _log.Info(ex);
                return View("Error");
            }
        }


        public IEnumerable<SelectListItem> GetEmailTemplateToken(string code)
        {

            var types = _EmailTokenQuery.GetAllList(c => c.EmailCode == code).Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Token.ToString(),
                                    Text = x.Token
                                });

            return new SelectList(types, "Value", "Text");
        }

        public async Task<ActionResult> Edit(int id)
        {
            EditViewBagParams();
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailTemplate emailTemplate =await  _EmailTemplateQuery.GetAsync(id);
            var emailTokenModel = _EmailTokenQuery.GetAllList(c => c.EmailCode == emailTemplate.Code).ToList();
            try
            {
                var model = new EmailViewModel()
                {
                    EmailID = emailTemplate.Id,
                    EmailSubject = emailTemplate.Name,
                    EmailCode = emailTemplate.Code,
                    EmailText = emailTemplate.Body,
                    EmailToken = emailTokenModel 

                };
                return PartialView("_PartialAddEdit", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _log.Info(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, EmailViewModel systememail)
        {
            try
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (ModelState.IsValid)
                {
                    EmailTemplate emailTemplate = await _EmailTemplateQuery.GetAsync(id);
                    emailTemplate.Body = systememail.EmailText;
                    await _EmailTemplateCommand.UpdateAsync(emailTemplate);
                    await _EmailTemplateCommand.SaveChangesAsync();
                    TempData["MESSAGE"] = "Email Template " + emailTemplate.Name + " was successfully updated";
                    ModelState.Clear();
                    return Json(new { success = true });
                }
                else
                {
                    foreach (var modelError in ModelState.Values.SelectMany(modelState => modelState.Errors))
                    {
                        ModelState.AddModelError(string.Empty, modelError.ErrorMessage);
                    }
                    return PartialView("_PartialAddEdit", systememail);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _log.Info(ex);
                return View("Error");
            }
        }


    }
}
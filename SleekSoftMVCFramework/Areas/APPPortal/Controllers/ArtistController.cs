using log4net;
using SleekSoftMVCFramework.Controllers;
using SleekSoftMVCFramework.Data.Constant;
using SleekSoftMVCFramework.Data.IdentityModel;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Repository.CoreRepositories;
using SleekSoftMVCFramework.Utilities;
using SleekSoftMVCFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SleekSoftMVCFramework.Areas.APPPortal.Controllers
{
    public class ArtistController : BaseController
    {
        private readonly IRepositoryQuery<ApplicationUser, long> _applicationUserQuery;
        private readonly IRepositoryCommand<ApplicationUser, long> _applicationUserCommand;
        private readonly IRepositoryCommand<ApplicationUserPasswordHistory, long> _applicationUserPwdhistoryCommand;
        private readonly IActivityLogRepositoryCommand _activityRepo;
        private readonly ILog _log;
        private readonly Utility _utility;

        public ArtistController(IActivityLogRepositoryCommand activityRepo, IRepositoryCommand<ApplicationUser, long> applicationUserCommand, Utility utility, IRepositoryQuery<ApplicationUser, long> applicationUser, IRepositoryCommand<ApplicationUserPasswordHistory, long> applicationUserPwdhistory, ILog log)
        {

            _applicationUserQuery = applicationUser;
            _applicationUserCommand = applicationUserCommand;
            _applicationUserPwdhistoryCommand = applicationUserPwdhistory;
            _activityRepo = activityRepo;
            _utility = utility;
            _log = log;
        }

        // GET: Portal/PortalUser
        public async Task<ActionResult> Index()
        {
            try
            {
                _log.Info("<<< In Portal User Page >>>");
                if (TempData["MESSAGE"] != null)
                {
                    ViewBag.Msg = TempData["MESSAGE"] as string;
                }
                var usermodel = await  _applicationUserQuery.ExecuteStoredProdure<ArtistViewModel>("spGetArtistUser").ToListAsync();
                return View(usermodel);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return View("Error");
            }

        }


        // GET: ApplicationRoles/Create
        public ActionResult Create()
        {
            CreateViewBagParams();
            return PartialView("_PartialAddEdit", new ArtistViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ArtistViewModel model, HttpPostedFileBase profileImage)
        {
            string code = string.Empty;
            string profilePath = string.Empty;
            try
            {
                CreateViewBagParams();
                if (ModelState.IsValid)
                {

                    if (profileImage != null && profileImage.ContentLength > 0)
                    {
                        var ext = Path.GetExtension(profileImage.FileName).Trim().ToLower();
                        string[] allowedExtension = new string[] { ".jpeg", ".jpg", ".png" };
                        if (allowedExtension.Contains(ext))
                        {
                            profilePath = _utility.Upload(profileImage, _utility.GetAppSetting("AppUploadFolder"));

                        }
                        else
                        {
                            ModelState.AddModelError("", string.Format("Invalid image extension,allowed extension are: .jpeg,.jpg,.png ", allowedExtension));
                            //return PartialView("_PartialAddEdit", staffVm);
                            return View("_PartialAddEdit", model);
                        }
                    }


                    //checking if emailaddress does not exist b4
                    var organizerAdminEmailExist = _applicationUserQuery.GetAllList(m => m.Email.ToLower().Trim() == model.Email.ToLower().Trim()).ToList();
                    if (organizerAdminEmailExist.Any())
                    {
                        ModelState.AddModelError("", "email address already exist");
                        return PartialView("_PartialAddEdit", model);
                    }

                    //checking if username does not exist b4
                    var organizerAdminUsernameExist = _applicationUserQuery.GetAllList(m => m.UserName.ToLower().Trim() == model.UserName.ToLower().Trim()).ToList();
                    if (organizerAdminUsernameExist.Any())
                    {
                        ModelState.AddModelError("", "username already exist");
                        return PartialView("_PartialAddEdit", model);
                    }

                    ApplicationUser usermodel = ArtistViewModel.ModeltoEntity(model);
                    usermodel.PicturePath = Path.GetFileName(profilePath);
                    usermodel.FacebookURL = model.FacebookURL;

                    var result = await UserManager.CreateAsync(usermodel, "Password");
                    if (result.Succeeded)
                    {
                        _activityRepo.CreateActivityLog(string.Format("Assinging User Id:{0} with Name :{1} to role Id's:{2}", usermodel.Id, (usermodel.LastName + " " + usermodel.FirstName), ""), this.GetContollerName(), this.GetContollerName(), usermodel.Id, null);

                        ApplicationUserPasswordHistory passwordModel = new ApplicationUserPasswordHistory();
                        passwordModel.UserId = usermodel.Id;
                        passwordModel.DateCreated = DateTime.Now;
                        passwordModel.HashPassword = ExtentionUtility.Encrypt("Password");
                        passwordModel.CreatedBy = usermodel.Id;
                        _applicationUserPwdhistoryCommand.Insert(passwordModel);
                        _applicationUserPwdhistoryCommand.Save();

                        var addRoleResult = await UserManager.AddToRoleAsync(usermodel.Id, "Artist");
                        if (addRoleResult.Succeeded)
                        {
                            //send user reset mail
                            code = await UserManager.GeneratePasswordResetTokenAsync(usermodel.Id);
                            string portalUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

                            var callbackUrl = Url.Action("ResetPassword", "Account", new { userCode = usermodel.Id.EncryptID(), code = code });
                            string mPre = portalUrl + callbackUrl;
                            _log.Info(string.Format("Reset URL:{0}", mPre));
                            if (!String.IsNullOrEmpty(usermodel.Email))
                            {
                                _utility.SendWelcomeAndPasswordResetEmail(usermodel, mPre);
                            }


                            TempData["MESSAGE"] = "Artist " + (usermodel.LastName + " " + usermodel.FirstName) + " was successfully created";
                            ModelState.Clear();
                            return Json(new { success = true });
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", result.Errors.FirstOrDefault().ToString());
                    }
                    ModelState.Clear();
                    return Json(new { success = true });

                }
                else
                {
                    StringBuilder errorMsg = new StringBuilder();

                    foreach (var modelError in ModelState.Values.SelectMany(modelState => modelState.Errors))
                    {
                        errorMsg.AppendLine(modelError.ErrorMessage);
                        ModelState.AddModelError(string.Empty, modelError.ErrorMessage);
                    }
                    ViewBag.ErrMsg = errorMsg.ToString();
                    return PartialView("_PartialAddEdit", model);
                }
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                //return View("Error");
                StringBuilder errorMsg = new StringBuilder();

                foreach (var modelError in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    errorMsg.AppendLine(modelError.ErrorMessage);
                    ModelState.AddModelError(string.Empty, modelError.ErrorMessage);
                }
                ViewBag.ErrMsg = errorMsg.ToString();
                return PartialView("_PartialAddEdit", model);
            }

        }


        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                EditViewBagParams();
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUser userModel = await _applicationUserQuery.GetAsync(id);
                ArtistViewModel userdataModel = ArtistViewModel.EntityToModels(userModel);
                if (userModel == null)
                {
                    return HttpNotFound();
                }
                return PartialView("_PartialAddEdit", userdataModel);
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(ArtistViewModel model, HttpPostedFileBase profileImage)
        {
            string code = string.Empty;
            string profilePath = string.Empty;
            try
            {
                EditViewBagParams();
                if (ModelState.IsValid)
                {


                    if (profileImage != null && profileImage.ContentLength > 0)
                    {
                        var ext = Path.GetExtension(profileImage.FileName).Trim().ToLower();
                        string[] allowedExtension = new string[] { ".jpeg", ".jpg", ".png" };
                        if (allowedExtension.Contains(ext))
                        {
                            profilePath = _utility.Upload(profileImage, _utility.GetAppSetting("AppUploadFolder"));

                        }
                        else
                        {
                            ModelState.AddModelError("", string.Format("Invalid image extension,allowed extension are: .jpeg,.jpg,.png ", allowedExtension));
                            //return PartialView("_PartialAddEdit", staffVm);
                            return View("_PartialAddEdit", model);
                        }
                    }

                    ApplicationUser usermodel = await _applicationUserQuery.GetAsync(model.Id);
                    if (usermodel != null)
                    {
                        usermodel.FirstName = model.FirstName;
                        usermodel.LastName = model.LastName;
                        usermodel.MiddleName = model.MiddleName;
                        usermodel.MobileNumber = model.MobileNumber;
                        usermodel.PhoneNumber = model.PhoneNumber;
                        usermodel.DOB = !string.IsNullOrEmpty(model.DOB) ? ExtentionUtility.ConvertDateValue(model.DOB) : DateTime.MinValue;
                        usermodel.Address = model.Address;
                        usermodel.PicturePath = Path.GetFileName(profilePath);
                        usermodel.FacebookURL = model.FacebookURL;
                        await _applicationUserCommand.UpdateAsync(usermodel);
                        await _applicationUserCommand.SaveChangesAsync();
                        _activityRepo.CreateActivityLog(string.Format("Updtae User Id:{0} with Name :{1}", usermodel.Id, (usermodel.LastName + " " + usermodel.FirstName)), this.GetContollerName(), this.GetActionName(), usermodel.Id, null);
                        
                        _activityRepo.CreateActivityLog(string.Format("Assinging User Id:{0} with Name :{1} to role Id's:{2}", usermodel.Id, (usermodel.LastName + " " + usermodel.FirstName), ""), this.GetContollerName(), this.GetActionName(), usermodel.Id, null);

                        TempData["MESSAGE"] = "Artist " + (usermodel.LastName + " " + usermodel.FirstName) + " was successfully created";
                        ModelState.Clear();
                        return Json(new { success = true });

                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while processing your request");
                    }
                    ModelState.Clear();
                    return Json(new { success = true });

                }
                else
                {
                    StringBuilder errorMsg = new StringBuilder();

                    foreach (var modelError in ModelState.Values.SelectMany(modelState => modelState.Errors))
                    {
                        errorMsg.AppendLine(modelError.ErrorMessage);
                        ModelState.AddModelError(string.Empty, modelError.ErrorMessage);
                    }
                    ViewBag.ErrMsg = errorMsg.ToString();
                    return PartialView("_PartialAddEdit", model);
                }
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                //return View("Error");
                StringBuilder errorMsg = new StringBuilder();

                foreach (var modelError in ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    errorMsg.AppendLine(modelError.ErrorMessage);
                    ModelState.AddModelError(string.Empty, modelError.ErrorMessage);
                }
                ViewBag.ErrMsg = errorMsg.ToString();
                return PartialView("_PartialAddEdit", model);
            }

        }


        // GET: ApplicationUser/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ApplicationUser usermodel = null;
            try
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                usermodel = await _applicationUserQuery.GetAsync(id);
                if (usermodel == null)
                {
                    return HttpNotFound();
                }
                return View(usermodel);
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }

        }

        // POST: ApplicationUser/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ApplicationUser user)
        {
            try
            {
                user = await _applicationUserQuery.GetAsync(id);
                if (user.Id == 1)
                {
                    ModelState.AddModelError("", "You cannot delete this artist.");
                    return RedirectToAction("Index");
                }
                user.IsDeleted = true;
                await _applicationUserCommand.UpdateAsync(user);
                await _applicationUserCommand.SaveChangesAsync();
                TempData["MESSAGE"] = "Artist " + user.LastName + " " + user.FirstName + " was successfully deleted";
                ModelState.Clear();
                return RedirectToAction("Index");

            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }
        }

    }
}
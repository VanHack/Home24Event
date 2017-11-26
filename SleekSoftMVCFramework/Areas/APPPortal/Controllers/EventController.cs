using log4net;
using SleekSoftMVCFramework.Controllers;
using SleekSoftMVCFramework.Data.AppEntities;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Repository.CoreRepositories;
using SleekSoftMVCFramework.Utilities;
using SleekSoftMVCFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SleekSoftMVCFramework.Areas.APPPortal.Controllers
{
    public class ScheduleViewModel
    {
        public Int64 Id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string title { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
    }
    public class EventController : BaseController
    {
        private readonly Utility _utility;
        private readonly IRepositoryQuery<Event,long> _EventQuery;
        private readonly IRepositoryCommand<Event, long> _EventCommand;

        private readonly IActivityLogRepositoryCommand _activityRepo;
        private readonly ILog _log;


        public EventController(IActivityLogRepositoryCommand activityRepo, IRepositoryQuery<Event, long> eventQuery, IRepositoryCommand<Event, long> eventCommand, Utility utility, ILog log)
        {
            _activityRepo = activityRepo;
            _utility = utility;
            _EventQuery = eventQuery;
            _EventCommand = eventCommand;
            _log = log;
        }
        // GET: EventApp/Event
        public async Task<ActionResult> Index()
        {
            try
            {
                if (TempData["MESSAGE"] != null)
                {
                    ViewBag.Msg = TempData["MESSAGE"] as string;
                }
                List<EventViewModel> eventmodel = await _EventQuery.ExecuteStoredProdure<EventViewModel>("spGetEvents").ToListAsync();
                _activityRepo.CreateActivityLog(string.Format("User ID:{0} viewed event list count of {1}", GetCurrentUserID(), eventmodel.Count), this.GetContollerName(), this.GetActionName(), GetCurrentUserID(), null);
                return View(eventmodel);
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }
        }


        public ActionResult Create()
        {
            CreateViewBagParams();
            EventViewModel eventVm = new EventViewModel();
            eventVm.StartDate = null;
            eventVm.EndDate = null;
            eventVm.Artists = _utility.GetAllArtists();
            return PartialView("_PartialAddEdit", eventVm);
        }

        // POST: Class/Create
        [HttpPost]
        public async Task<ActionResult> Create(EventViewModel eventVm)
        {
            string filename = string.Empty;
            string fileName = string.Empty;
            eventVm.Artists = _utility.GetAllArtists();
            try
            {
                CreateViewBagParams();
                if (ModelState.IsValid)
                {
                    //checking if organizer name does not exist b4
                    var eventnameExist = _EventQuery.GetAllList(m => m.EventName.ToLower().Trim() == eventVm.EventName.ToLower().Trim()).ToList();
                    if (eventnameExist.Any())
                    {
                        ModelState.AddModelError("", "Event name already exist");
                        return PartialView("_PartialAddEdit", eventVm);
                    }

                    var eventmodel = new Event()
                    {
                        EventName = eventVm.EventName,
                        EventDescription = eventVm.EventDescription,
                        Venue = eventVm.Venue,
                        ArtistId = eventVm.ArtistId,
                        City=eventVm.City,
                        Country=eventVm.Country,
                        StartDate = ExtentionUtility.ConvertDateValue(eventVm.StartDate),
                        EndDate = ExtentionUtility.ConvertDateValue(eventVm.EndDate),
                        CreatedBy = GetCurrentUserID()
                    };
                    //Save Event
                    await _EventCommand.InsertAsync(eventmodel);
                    await _EventCommand.SaveChangesAsync();

                    _activityRepo.CreateActivityLog(string.Format("User ID: {0} Created event with event Name:{0}", GetCurrentUserID(), eventmodel.EventName), this.GetContollerName(), this.GetActionName(), GetCurrentUserID(), eventmodel);

                    TempData["MESSAGE"] = "Event " + eventmodel.EventName + " was successfully created";
                    ModelState.Clear();
                    return Json(new { success = true });
                }
                else
                {
                    StringBuilder errorMsg = new StringBuilder();

                    var errorModel =
                          from x in ModelState.Keys
                          where ModelState[x].Errors.Count > 0
                          select new
                          {

                              key = x,
                              errors = ModelState[x].Errors.
                                                     Select(y => y.ErrorMessage).
                                                     ToArray()
                          };

                    foreach (var item in errorModel)
                    {
                        errorMsg.AppendLine(string.Format("Error Key: {0} Error Message: {1}", item.key, string.Join(",", item.errors)));
                        ModelState.AddModelError(item.key, string.Join(",", item.errors));
                    }
                    ViewBag.ErrMsg = errorMsg.ToString();
                    return PartialView("_PartialAddEdit", eventVm);
                }

            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }


        }


        public async Task<ActionResult> Edit(int id)
        {
            EventViewModel eventVm = new EventViewModel();
            eventVm.Artists = _utility.GetAllArtists();
            try
            {
                EditViewBagParams();
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Event eventModel = await _EventQuery.GetAsync(id);
                if (eventModel == null)
                {
                    return HttpNotFound();
                }
               
                eventVm.Id = eventModel.Id;

                eventVm.EventName = eventModel.EventName;
                eventVm.EventDescription = eventModel.EventDescription;
                eventVm.Venue = eventModel.Venue;
                eventVm.ArtistId = eventModel.ArtistId;
                eventVm.Venue = eventModel.Venue;
                eventVm.City = eventModel.City;
                eventVm.Country = eventModel.Country;
                eventVm.StartDate = eventModel.StartDate.ToString("dd/MM/yyyy hh:mm tt");
                eventVm.EndDate = eventModel.EndDate.ToString("dd/MM/yyyy hh:mm tt");
                return PartialView("_PartialAddEdit", eventVm);
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EventViewModel eventVm)
        {
            string filename = string.Empty;
            string fileName = string.Empty;
            Int64 CurrentUserId = GetCurrentUserID();
            eventVm.Artists = _utility.GetAllArtists();
            try
            {
                EditViewBagParams();
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (ModelState.IsValid)
                {
                    //checking if organizer name does not exist b4
                    var eventnameExist = _EventQuery.GetAllList(m => m.EventName.ToLower().Trim() == eventVm.EventName.ToLower().Trim() && m.Id != eventVm.Id).ToList();
                    if (eventnameExist.Any())
                    {
                        ModelState.AddModelError("", "Event name already exist");
                        return PartialView("_PartialAddEdit", eventVm);
                    }

                    Event eventModel = await _EventQuery.GetAsync(eventVm.Id);
                    eventModel.EventName = eventVm.EventName;
                    eventModel.EventDescription = eventVm.EventDescription;
                    eventModel.Venue = eventVm.Venue;
                    eventModel.ArtistId = eventVm.ArtistId;
                    eventModel.Venue = eventVm.Venue;
                    eventModel.City = eventVm.City;
                    eventModel.Country = eventVm.Country;
                    eventModel.StartDate = ExtentionUtility.ConvertDateValue(eventVm.StartDate);
                    eventModel.EndDate = ExtentionUtility.ConvertDateValue(eventVm.EndDate);

                    //Save Event
                    await _EventCommand.UpdateAsync(eventModel);
                    await _EventCommand.SaveChangesAsync();

                    _activityRepo.CreateActivityLog(string.Format(" User Id:{0} updated event with event Name:{1}", CurrentUserId, eventVm.EventName), this.GetContollerName(), this.GetActionName(), CurrentUserId, eventVm);

                    TempData["MESSAGE"] = "Event: " + eventVm.EventName + " was successfully updated";
                    ModelState.Clear();

                    return Json(new { success = true });
                }
                else
                {
                    StringBuilder errorMsg = new StringBuilder();

                    var errorModel =
                          from x in ModelState.Keys
                          where ModelState[x].Errors.Count > 0
                          select new
                          {

                              key = x,
                              errors = ModelState[x].Errors.
                                                     Select(y => y.ErrorMessage).
                                                     ToArray()
                          };

                    foreach (var item in errorModel)
                    {
                        errorMsg.AppendLine(string.Format("Error Key: {0} Error Message: {1}", item.key, string.Join(",", item.errors)));
                        ModelState.AddModelError(item.key, string.Join(",", item.errors));
                    }
                    //ModelState.Values.SelectMany(modelState => modelState.Errors)
                    //foreach (var modelError in ModelState.Erro)
                    //{
                    //    errorMsg.AppendLine(modelError.ErrorMessage);
                    //    ModelState.AddModelError(string.Empty, modelError.ErrorMessage);
                    //}
                    ViewBag.ErrMsg = errorMsg.ToString();
                    return PartialView("_PartialAddEdit", eventVm);
                }
            }
            catch (Exception exp)
            {
                _log.Error(exp);
                return View("Error");
            }

        }


        public JsonResult GetEvents(ScheduleViewModel scheduleVm)
        {
            DateTime sdate = DateTime.Now;
            DateTime edate = DateTime.Now;
            DateTime.TryParse(scheduleVm.StartDate, out sdate);
            DateTime.TryParse(scheduleVm.EndDate, out edate);
            if (sdate == DateTime.MinValue) sdate = DateTime.Now;
            if (edate == DateTime.MinValue) edate = ExtentionUtility.GetDateValue(DateTime.Now.ToString("yyyy-MM-") + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString());

            var model = new List<ScheduleViewModel>();
            List<Event> eventmodel = _EventQuery.GetAll().ToList();
            if (eventmodel.Any())
            {
                foreach (Event e in eventmodel)
                {
                    model.Add(new ScheduleViewModel()
                    {
                        Id = e.Id,
                        start = e.StartDate,
                        //eventVmodel.StartDate,
                        end = e.EndDate,
                        //eventVmodel.EndDate,
                        title = "Event: " + e.EventName +
                                "\nVenue: " + e.Venue +
                                "\n Description : " + e.EventDescription
                    });
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EventTimeTable()
        {
            return View();
        }
    }
}
using EventScheduler.Data;
using EventScheduler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EventScheduler.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _um;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _db, UserManager<IdentityUser> _um)
        {
            _logger = logger;
            this._db = _db;
            this._um = _um;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? year, int? month, int? day)
        {
            if (year == null)
            {
                ViewBag.Date = DateTime.Today;
            }
            else
            {
                DateTime? dt = getTime(year, month, day);
                if (dt == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Date = (DateTime)dt;
                }
            }
            var userid = HttpContext.User.Identity.Name;
            var user = await _um.FindByNameAsync(userid);

            List<CalendarEvent> model = getCalendarEvents(user);
            ViewBag.Notes = getNotes(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Week(int? year, int? month, int? day)
        {
            if (year == null)
            {
                ViewBag.Date = DateTime.Today;
            }
            else
            {
                DateTime? dt = getTime(year, month, day);
                if (dt == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Date = (DateTime)dt;
                }
            }
            var userid = HttpContext.User.Identity.Name;
            var user = await _um.FindByNameAsync(userid);

            List<CalendarEvent> model = getCalendarEvents(user);
            ViewBag.Notes = getNotes(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Day(int? year, int? month, int? day)
        {
            if (year == null)
            {
                ViewBag.Date = DateTime.Today;
            }
            else
            {
                DateTime? dt = getTime(year, month, day);
                if (dt == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Date = (DateTime)dt;
                }
            }
            var userid = HttpContext.User.Identity.Name;
            var user = await _um.FindByNameAsync(userid);

            List<CalendarEvent> model = getCalendarEvents(user);
            ViewBag.Notes = getNotes(user);
            
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private DateTime? getTime(int? year, int? month, int? day)
        {
            try
            {
                if (month == null)
                {
                    return new DateTime((int)year, 1, 1);
                }
                else
                {
                    if (day == null)
                    {
                        if (year == DateTime.Now.Year && month == DateTime.Now.Month)
                        {
                            return new DateTime((int)year, (int)month, DateTime.Now.Day);
                        }
                        else
                        {
                            return new DateTime((int)year, (int)month, 1);
                        }
                    }
                    else
                    {
                        return new DateTime((int)year, (int)month, (int)day);
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                return null;
            }
        }

        private List<CalendarEvent> getCalendarEvents(IdentityUser user)
        {
            List<CalendarEvent> modelx = _db.CalendarEvents.ToList<CalendarEvent>();
            List<CalendarEvent> model = new List<CalendarEvent>();
            foreach (var i in modelx)
            {
                if (i.User == user)
                {
                    model.Add(i);
                }
            }
            return model;
        }

        private dynamic getNotes(IdentityUser user)
        {
            List<Note> notes = _db.Notes.ToList<Note>();
            List<Note> notesmodel = new List<Note>();
            foreach (var i in notes)
            {
                if (i.User == user)
                {
                    notesmodel.Add(i);
                }
            }
            return notesmodel;
        }

        [HttpPost, ActionName("DeleteEvent")]
        public async Task<ActionResult> DeleteEvent(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var eventtodelete = _db.CalendarEvents.FirstOrDefault(x => x.Id == id);
            if (eventtodelete == null)
            {
                return NotFound();
            }
            _db.CalendarEvents.Remove(eventtodelete);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteNote")]
        public async Task<ActionResult> DeleteNote(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var notetodelete = _db.Notes.FirstOrDefault(x => x.Id == id);
            if (notetodelete == null)
            {
                return NotFound();
            }
            _db.Notes.Remove(notetodelete);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent()
        {
            CalendarEvent ce = new CalendarEvent();
            ce.Date = DateTime.Now;
            return PartialView("AddEventPartial", ce);
        }

        [HttpGet]
        public async Task<IActionResult> GetNote()
        {
            Note n = new Note();
            n.Description = "Tutaj wpisz treść notatki";
            return PartialView("AddNotePartial", n);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventById(string id)
        { 
            if(id == null)
            {
                return NotFound();
            }
            CalendarEvent ce = _db.CalendarEvents.FirstOrDefault(x => x.Id == id);
            return PartialView("EventDetailsPartial", ce);
        }

        [HttpGet]
        public async Task<IActionResult> GetNoteById(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Note n = _db.Notes.FirstOrDefault(x => x.Id == id);
            return PartialView("NoteDetailsPartial", n);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CalendarEvent ce)
        {
            var userid = HttpContext.User.Identity.Name;
            var user = await _um.FindByNameAsync(userid);
            ce.Id = Guid.NewGuid().ToString();
            ce.User = user;
            await _db.CalendarEvents.AddAsync(ce);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(Note n)
        {
            var userid = HttpContext.User.Identity.Name;
            var user = await _um.FindByNameAsync(userid);
            n.Id = Guid.NewGuid().ToString();
            n.User = user;
            await _db.Notes.AddAsync(n);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(CalendarEvent ce)
        {
            _db.CalendarEvents.Update(ce);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditNote(Note n)
        {
            _db.Notes.Update(n);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

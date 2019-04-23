using Lab_4.Data;
using Lab_4.Filters;
using Lab_4.Models;
using Lab_4.Services;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_4.Controllers
{
    public class CinemaController : Controller
    {

        private CinemaContext _db;
        private IMemoryCache _cache;
        private string _cookieKey = "formCookies";
        private DbService _service;

        public CinemaController(CinemaContext context, IMemoryCache memoryCache, DbService service)
        {
            _db = context;
            _cache = memoryCache;
            _service = service;
        }

        [LoggingFilter]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [SaveStateFilter("sort")]
        [LoggingFilter]
        public async Task<IActionResult> CinemaList(string name = null, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            Dictionary<string, string> dict;
            if(HttpContext.Session.Keys.Contains("sort"))
            {
                dict = HttpContext.Session.Get<Dictionary<string, string>>("sort");
                if (dict.ContainsKey("name")) name = dict["name"];
                if (dict.ContainsKey("sortOrder")) sortOrder = Enum.Parse<SortState>(dict["sortOrder"]);
            }

            int pageSize = 10;

            ViewData["Message"] = "Cinema-List";

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ViewData["DurationSort"] = sortOrder == SortState.DurationAsc ? SortState.DurationDesc : SortState.DurationAsc;

            IQueryable<Cinema> cacheList = _service.GetCinemas(name, sortOrder);

            var count = await cacheList.CountAsync();
            var items = await cacheList.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CinemaViewModel viewModel = new CinemaViewModel
            {
                PageViewModel = pageViewModel,
                cinemas = items,
                Name = name
            };

            return View(viewModel);
        }

        [Authorize(Roles = "manager")]
        [LoggingFilter]
        public IActionResult AddCinema()
        {
            ViewData["Message"] = "Add cinema date";
            List<string> NameCinema = new List<string> { "Терминатор", "СуперНянь", "ГарриПоттер", "Пила 8" };
            ViewBag.NameCinema = new SelectList(NameCinema);
            Cinema cinema = new Cinema("Терминатор", 0, DateTime.Now, 0);

            if(Request.Cookies.ContainsKey(_cookieKey))
            {
                cinema = JsonConvert.DeserializeObject<Cinema>(Request.Cookies[_cookieKey]);
            }

            return View(cinema);
        }

        [LoggingFilter]
        [HttpPost]
        public IActionResult AddCinema(Cinema cinema)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddSeconds(2 * 8 + 240);
            Response.Cookies.Append(_cookieKey, JsonConvert.SerializeObject(cinema), option);
            _service.AddCinema(cinema);
            _cache.Remove("Cinema");

            return View("CinemaList", _service.GetCinemas());
        }

        [Authorize(Roles = "manager")]
        public IActionResult Edit(int id)
        {
            Cinema cinema = _service.FindCinemaById(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }

        [HttpPost]
        public IActionResult Edit(Cinema model)
        {
            if (ModelState.IsValid)
            {
                Cinema cinema = _service.FindCinemaById(model.Id);
                if (cinema != null)
                {
                    cinema.Name = model.Name;
                    cinema.Age = model.Age;

                    _service.UpdateCinema(cinema);
                    return RedirectToAction("CinemaList");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _service.DeleteCinema(id);
            return RedirectToAction("CinemaList");
        }

        [LoggingFilter]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
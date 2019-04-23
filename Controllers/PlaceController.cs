using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lab_4.Data;
using Lab_4.Models;
using Lab_4.Services;
using Lab_4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Lab_4.Controllers
{
    public class PlaceController : Controller
    {

        private CinemaContext _db;
        private IMemoryCache _cache;
        private string _sessionKey = "formSession";
        private DbService _service;

        public PlaceController(CinemaContext context, IMemoryCache memoryCache, DbService service)
        {
            _db = context;
            _cache = memoryCache;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Place(int page = 1, SortState sortOrder = SortState.SessionAsc)
        {
            int pageSize = 10;

            ViewData["Message"] = "Places";

            ViewData["SessionSort"] = sortOrder == SortState.SessionAsc ? SortState.SessionDesc : SortState.SessionAsc;
            ViewData["PlaceNumberSort"] = sortOrder == SortState.PlaceNumberAsc ? SortState.PlaceNumberDesc : SortState.PlaceNumberAsc;

            var places = _service.GetPlaces(sortOrder);

            var count = await places.CountAsync();
            var items = await places.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PlaceViewModel viewModel = new PlaceViewModel
            {
                PageViewModel = pageViewModel,
                places = items
            };

            return View(viewModel);
        }

        [Authorize(Roles = "manager")]
        public IActionResult AddPlace()
        {
            ViewData["Message"] = "Add place";
            Place place = new Place();

            if (HttpContext.Session.Keys.Contains(_sessionKey))
            {
                place = JsonConvert.DeserializeObject<Place>(HttpContext.Session.GetString(_sessionKey));
            }

            return View(place);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlace(Place place)
        {
            _db.Places.Add(place);
            _db.SaveChanges();
            HttpContext.Session.SetString(_sessionKey, JsonConvert.SerializeObject(place));

            return View("Place", await _db.Places.ToListAsync());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
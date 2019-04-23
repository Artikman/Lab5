using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab_4.Models;
using Lab_4.Data;
using Microsoft.Extensions.Caching.Memory;
using Lab_4.Services;

namespace Lab_4.Controllers
{
    public class HomeController : Controller
    {
        private CinemaContext _db;
        private IMemoryCache _cache;
        private string _cookieKey = "formCookies";
        private string _sessionKey = "formSession";
        private DbService _service;

        public HomeController(CinemaContext context, IMemoryCache memoryCache, DbService service)
        {
            _db = context;
            _cache = memoryCache;
            _service = service;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
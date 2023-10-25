using Microsoft.AspNetCore.Mvc;
using StormShift.Models;
using StormShift.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StormShift.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsService _newsService;

        public HomeController(ILogger<HomeController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var headlines = await _newsService.GetTopHeadlines();
            return View(headlines);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

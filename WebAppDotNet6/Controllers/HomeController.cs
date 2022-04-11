using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppDotNet6.Models;

namespace WebAppDotNet6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            AppSettingsProvider ap = new(_configuration);
            IDictionary<string, string> result = ap.GetAppSettings();
            AppSettingsModel appSettingsModel = new(result);

            // Add the model to the ViewBag to be used in the Index.cshtml view
            ViewBag.AppSettings = appSettingsModel;

            return View();
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
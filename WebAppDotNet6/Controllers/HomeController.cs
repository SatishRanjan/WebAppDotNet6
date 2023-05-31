using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
            HttpContextAccessor HttpContextAccessor = new HttpContextAccessor();
            var uri_1 = new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}").GetLeftPart(UriPartial.Authority);
            AppSettingsProvider ap = new(_configuration);
            IDictionary<string, string> result = ap.GetAppSettings();
            AppSettingsModel appSettingsModel = new(result);

            // Add the model to the ViewBag to be used in the Index.cshtml view
            ViewBag.AppSettings = appSettingsModel;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                CertificatesProvider cp = new CertificatesProvider();
                ViewBag.CertificatesSubjectName = cp.GetCertificatesSubjectName();

                ViewBag.CertsEnvVariables = GetCertsEnvironmentVariables();
            }

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

        private Dictionary<string, string?> GetCertsEnvironmentVariables()
        {
            Dictionary<string, string?> certificatesEnvVariables = new Dictionary<string, string?>();
            certificatesEnvVariables.Add("WEBSITE_PRIVATE_CERTS_PATH", Environment.GetEnvironmentVariable("WEBSITE_PRIVATE_CERTS_PATH"));
            certificatesEnvVariables.Add("WEBSITE_PUBLIC_CERTS_PATH", Environment.GetEnvironmentVariable("WEBSITE_PUBLIC_CERTS_PATH"));
            certificatesEnvVariables.Add("WEBSITE_INTERMEDIATE_CERTS_PATH", Environment.GetEnvironmentVariable("WEBSITE_INTERMEDIATE_CERTS_PATH"));
            certificatesEnvVariables.Add("WEBSITE_ROOT_CERTS_PATH", Environment.GetEnvironmentVariable("WEBSITE_ROOT_CERTS_PATH"));

            return certificatesEnvVariables;
        }
    }
}
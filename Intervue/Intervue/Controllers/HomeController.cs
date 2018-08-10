using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Intervue.Models;

namespace Intervue.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// returns to the Index View (Home View folder, Index page)
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Directs to the About page (Home View folder, About page)
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// This is the error page in case any other view than above is attempted
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
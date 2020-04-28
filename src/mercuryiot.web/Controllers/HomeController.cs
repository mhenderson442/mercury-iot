using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Mercuryiot.Web.Controllers
{
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) => _logger = logger;

        public async Task<IActionResult> Index()
        {
            // TODO Remove
            var viewName = await Task.Run(() => "Index");

            return View(viewName);
        }

        public async Task<IActionResult> Error()
        {
            // TODO Remove
            await Task.Yield();

            return View();
        }
    }
}
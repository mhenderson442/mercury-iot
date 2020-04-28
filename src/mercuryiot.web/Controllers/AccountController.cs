using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mercuryiot.Web.Controllers
{
    public class AccountController : Controller
    {
        ILogger<AccountController> _logger;

        public AccountController()
        {
        }

        public AccountController(ILogger<AccountController> logger) => _logger = logger;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync()
        {
            // TODO Remove
            var viewName = await Task.Run(() => "LoginAsync");

            return View(viewName);
        }
    }
}
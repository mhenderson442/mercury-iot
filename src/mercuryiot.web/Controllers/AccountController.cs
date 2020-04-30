using System;
using System.Threading.Tasks;
using Mercuryiot.Web.Models;
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
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            // TODO Remove
            var viewName = await Task.Run(() => "LoginAsync");

            return View(viewName);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl = null){
            
            // TODO Remove
            var viewName = await Task.Run(() => "LoginAsync");
            
            return View(viewName, model);
        }
    }
}
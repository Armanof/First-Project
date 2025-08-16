using Bulky_Core.Interfaces;
using Bulky_Core.Messages;
using Bulky_DTO.Account;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IServiceContainer serviceContainer): base(serviceContainer) { }
        

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterUserDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO input)
        {
            return Ok();
        }
    }
}

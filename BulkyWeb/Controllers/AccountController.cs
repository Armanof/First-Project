using Bulky_DTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class AccountController : Controller
    {
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
    }
}

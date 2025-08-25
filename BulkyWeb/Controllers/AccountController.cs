using Bulky_Core.Interfaces;
using Bulky_Core.Messages;
using Bulky_Core.Utilities;
using Bulky_DTO.Account;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IConfiguration config;

        public AccountController(IServiceContainer serviceContainer,IConfiguration config): base(serviceContainer)
        {
            this.config = config;
        }


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
            var user = await serviceContainer.UserService().Register(input);
            if (user == null)
                return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });

            var token = serviceContainer.AuthService().GenerateJWTToken(user);

            Response.SetJwtCookie(token, Convert.ToInt32(config["Jwt:ExpireMinutes"]));

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO input)
        {
            var user = await serviceContainer.UserService().Login(input);
            if (user == null)
                return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });

            var token = serviceContainer.AuthService().GenerateJWTToken(user);

            Response.SetJwtCookie(token, Convert.ToInt32(config["Jwt:ExpireMinutes"]));

            return RedirectToAction("Index", "Home");
        }
    }
}

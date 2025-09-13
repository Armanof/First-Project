using Bulky_Core.Interfaces;
using Bulky_DTO.Identity;
using Bulky_Models.Identity;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IServiceContainer serviceContainer) : base(serviceContainer)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await serviceContainer.GenericCrudBaseService<User,UserDTO>().GetAll();
            return View(users);
        }
    }
}

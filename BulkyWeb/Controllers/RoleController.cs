using Bulky_Core.Interfaces;
using Bulky_DTO.Identity;
using Bulky_Models.Identity;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class RoleController : GenericCrudBaseController<Role,RoleDTO>
    {
        public RoleController(IServiceContainer serviceContainer) : base(serviceContainer)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await base.GetAll();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            if (id == null)
            {
                return PartialView("_RoleFormPartial");
            }
            else
            {
                var role = await serviceContainer.GenericCrudBaseService<Role, RoleDTO>().FirstOrDefault(x => x.Id == id);
                return PartialView("_RoleFormPartial", role);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(RoleDTO input)
        {
            return await base.Upsert(input);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}

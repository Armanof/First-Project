using Bulky_Core.Interfaces;
using Bulky_Core.Messages;
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

        [HttpPut()]
        public async Task<IActionResult> SetDefault(Guid? id)
        {
            var roleService = serviceContainer.GenericCrudBaseService<Role, RoleDTO>();

            if (id == null || id == default)
                return BadRequest(GeneralMessages.DefaultError());

            var role = await roleService.FirstOrDefault(x => x.Id == id);
            if(role == null)
                return BadRequest(GeneralMessages.DefaultError());

            var previousDefualtRole = await roleService.FirstOrDefault(x => x.IsDefault == true);
            if (previousDefualtRole != null)
            {
                previousDefualtRole.IsDefault = false;
                var result = await roleService.Update(previousDefualtRole);
                if(!result)
                    return BadRequest(GeneralMessages.DefaultError());
            }

            role.IsDefault = true;
            var result2 = await roleService.Update(role);
            if (!result2)
                return BadRequest(GeneralMessages.DefaultError());

            return Ok(result2);
        }
    }
}

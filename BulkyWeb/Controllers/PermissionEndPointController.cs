using Bulky_Core.Interfaces;
using Bulky_Core.Messages;
using Bulky_DTO.Identity;
using Bulky_Models.Identity;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class PermissionEndPointController : GenericCrudBaseController<PermissionEndPoint, PermissionEndPointDTO>
    {
        public PermissionEndPointController(IServiceContainer serviceContainer) : base(serviceContainer)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            if (id != null)
                return BadRequest(GeneralMessages.UnAuthorized);

            return PartialView("_PermissionEndPointPartialForm");
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(PermissionEndPointDTO input)
        {
            return await base.Upsert(input);
        }

        public async Task CreateViewLists()
        {
            ViewBag.PermissionList = await serviceContainer.GenericCrudBaseService<Permission, PermissionDTO>().GetAll();
            ViewBag.PermissionEndPoint = await serviceContainer.GenericCrudBaseService<PermissionEndPoint, PermissionEndPointDTO>().GetAll();
        }
    }
}

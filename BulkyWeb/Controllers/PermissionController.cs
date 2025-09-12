using Bulky_Core.Interfaces;
using Bulky_DTO.Identity;
using Bulky_Models.Identity;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BulkyWeb.Controllers
{
    public class PermissionController : GenericCrudBaseController<Permission, PermissionDTO>
    {
        public PermissionController(IServiceContainer serviceContainer) : base(serviceContainer)
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
            await CreateViewLists();
            var permission = await serviceContainer.GenericCrudBaseService<Permission, PermissionDTO>().FirstOrDefault(x => x.Id == id, include: x => x.Include(it => it.PermissionEndPoints));
            if (permission == null)
                permission = new PermissionDTO();

            return PartialView("_PermissionPartialForm", permission);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(PermissionDTO input)
        {
            return await base.Upsert(input);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        public async Task CreateViewLists()
        {
            ViewBag.PermissionList = await serviceContainer.GenericCrudBaseService<Permission, PermissionDTO>().GetAll();
            ViewBag.PermissionEndPoint = await serviceContainer.GenericCrudBaseService<PermissionEndPoint, PermissionEndPointDTO>().GetAll();
        }
    }
}

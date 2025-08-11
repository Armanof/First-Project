using Bulky_Core.Interfaces;
using Bulky_DTO;
using Bulky_Models;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BulkyWeb.Controllers
{
    public class CategoryController 
        : GenericCrudBaseController<Category,CategoryDTO>
    {
        public CategoryController(IServiceContainer serviceContainer) : base(serviceContainer)
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await GetAll();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            if (id == null)
            {
                return PartialView("_CategoryFormPartial");
            }
            else 
            {
                var category = await serviceContainer.GenericCrudBaseService<Category,CategoryDTO>().FirstOrDefault(x => x.Id == id);
                return PartialView("_CategoryFormPartial", category);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CategoryDTO input)
        {
            return await base.Upsert(input);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpGet]
        public async Task<IActionResult> FindById(Guid id)
        {
            return await base.FindById(id);
        }
    }
}

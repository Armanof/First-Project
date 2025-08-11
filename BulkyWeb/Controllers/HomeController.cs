
using System.Diagnostics;
using System.Threading.Tasks;
using Bulky.Models;
using Bulky_Core.Interfaces;
using Bulky_DTO;
using Bulky_Models;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(IServiceContainer serviceContainer) : base(serviceContainer)
        {
        }

        public async Task<IActionResult> Index()
        {
            var result = await serviceContainer.GenericCrudBaseService<Product, ProductDTO>().GetAll();
            if(result != null)
                return View(result);

            return View(new List<ProductDTO>());
        }
    }
}

using Bulky_Core.Interfaces;
using Bulky_Core.Utilities;
using Bulky_DTO;
using Bulky_Models;
using BulkyWeb.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class ProductController
        : GenericCrudBaseController<Product, ProductDTO>
    {
        private readonly string rootPath;
        private readonly string filePath;

        public ProductController(IServiceContainer serviceContainer, 
                                 IWebHostEnvironment webHostEnvironment,
                                 IConfiguration configuration) : base(serviceContainer)
        {
            rootPath = webHostEnvironment.WebRootPath;
            filePath = configuration["ImagesPath:Product"];
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await base.GetAll();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CreateViewLists();
            return PartialView("_ProductFormPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO input, IFormFile file)
        {
            try
            {
                input.ImageUrl = FileHelper.UploadFile(file, rootPath, filePath);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorCode = "F-001", ex.Message });
            }

            return await base.Add(input);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            await CreateViewLists();
            var product = await serviceContainer.GenericCrudBaseService<Product, ProductDTO>().FirstOrDefault(x => x.Id == id);
            return PartialView("_ProductFormPartial", product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO input, IFormFile file)
        {
            try
            {
                input.ImageUrl = FileHelper.UpdateFile(file, rootPath, filePath, input.ImageUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorCode = "F-001", ex.Message });
            }

            return await base.Edit(input);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await serviceContainer.GenericCrudBaseService<Product, ProductDTO>().FirstOrDefault(x => x.Id == id);
            if (product?.ImageUrl != null)
                FileHelper.RemoveFile(rootPath, filePath, product.ImageUrl);

            return await base.Delete(id);
        }

        [HttpGet]
        public async Task<IActionResult> FindById(Guid id)
        {
            return await base.FindById(id);
        }

        private async Task CreateViewLists()
        {
            ViewBag.CategoryList = await serviceContainer.GenericCrudBaseService<Category, CategoryDTO>().GetAll();
        }
    }
}

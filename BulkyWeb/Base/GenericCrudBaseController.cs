using Bulky_Core.Interfaces;
using Bulky_DTO;
using Bulky_DTO.Base;
using Bulky_Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BulkyWeb.Base
{
    public abstract class GenericCrudBaseController<TEntity, TDTO>
        : BaseController
        where TEntity : BaseModel, new()
        where TDTO : BaseDTO, new()
    {

        protected GenericCrudBaseController(IServiceContainer serviceContainer) : base(serviceContainer)
        {
        }

        public virtual async Task<IActionResult> GetAll()
        {
            var result = await serviceContainer.GenericCrudBaseService<TEntity, TDTO>().GetAll();
            if (result != null)
                return View(result);

            return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });
        }

        protected virtual async Task<IActionResult> Edit(TDTO input)
        {
            var result = await serviceContainer.GenericCrudBaseService<TEntity, TDTO>().Update(input);
            if (result)
                return Ok(result);

            return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });
        }

        protected virtual async Task<IActionResult> Add(TDTO input)
        {
            var result = await serviceContainer.GenericCrudBaseService<TEntity, TDTO>().Create(input);
            if (result)
                return Ok(result);

            return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });
        }

        protected virtual async Task<IActionResult> Delete(Guid id)
        {
            var result = await serviceContainer.GenericCrudBaseService<TEntity, TDTO>().Delete(id);
            if (result)
                return Ok(result);

            return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });
        }

        protected virtual async Task<IActionResult> FindById(Guid id)
        {
            var result = await serviceContainer.GenericCrudBaseService<TEntity, TDTO>().FirstOrDefault(x => x.Id == id);
            if (result != null)
                return Ok(result);

            return BadRequest(new { serviceContainer.ErrorCode, serviceContainer.ErrorMessage });
        }

        protected virtual async Task<IActionResult> Upsert(TDTO input)
        {
            if (input.Id == default)
            {
                return await Add(input);
            }
            else
            {
                return await Edit(input);
            }
        }
    }
}

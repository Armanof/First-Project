using Bulky_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BulkyWeb.Base
{
    public abstract class BaseController : Controller
    {
        protected readonly IServiceContainer serviceContainer;

        public BaseController(IServiceContainer serviceContainer)
        {
            this.serviceContainer = serviceContainer;
        }
    }
}

using Bulky_DTO.Identity;
using Bulky_Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Core.Validators.Identity
{
    public class LoginUserValidator
        : BaseValidator<LoginUserDTO>
    {
        public LoginUserValidator(IUnitOfWork uow) : base(uow)
        {
            
        }
    }
}

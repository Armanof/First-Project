using Bulky_DTO.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_DTO.Identity
{
    public class LoginUserDTO
        : BaseDTO
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}

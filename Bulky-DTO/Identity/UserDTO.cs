using Bulky_DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_DTO.Identity
{
    public class UserDTO
        : BaseDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string SaltPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SentCode { get; set; }
        public bool IsDeveloper { get; set; }
    }
}

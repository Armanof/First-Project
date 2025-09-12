using Bulky_DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_DTO.Identity
{
    public class PermissionEndPointDTO
        : BaseDTO
    {
        public string EndPointName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }

        public Guid? F_PermissionId { get; set; }
    }
}

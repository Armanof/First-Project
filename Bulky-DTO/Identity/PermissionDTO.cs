using Bulky_DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_DTO.Identity
{
    public class PermissionDTO
        : BaseDTO
    {
        public PermissionDTO()
        {
            PermissionEndPoints = new List<PermissionEndPointDTO>();
        }

        public string Name { get; set; }
        public Guid? F_ParentId { get; set; }

        public ICollection<PermissionEndPointDTO> PermissionEndPoints { get; set; }
    }
}

using Bulky_Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Models.Identity
{
    public class RolePermission
        :BaseModel
    {
        public Guid F_RoleId { get; set; }
        public Guid F_PermissionId { get; set; }


        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }

        public override void StringNormalize()
        {
        }
    }

    public class RolePermissionEntityTypeConfiiguration : BaseModelEntityTypeConfiguration<RolePermission>
    {
        public RolePermissionEntityTypeConfiiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.F_RoleId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.F_PermissionId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}

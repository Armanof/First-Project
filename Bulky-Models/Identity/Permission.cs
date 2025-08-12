using Bulky_Models.Base;
using Bulky_Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Models.Identity
{
    public class Permission
        : BaseModel
    {
        public string Name { get; set; }

        public Guid? F_ParentId { get; set; }


        public virtual Permission ParentPemission { get; set; }
        public virtual ICollection<Permission> ChildPermissions { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<PermissionEndPoint> PermissionEndPoints { get; set; }

        public override void StringNormalize()
        {
            Name = StringNormalization.NormalizeString(Name);
        }
    }

    public class PermissionEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Permission>
    {
        public PermissionEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.ParentPemission)
                .WithMany(x => x.ChildPermissions)
                .HasForeignKey(x => x.F_ParentId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }
    }
}

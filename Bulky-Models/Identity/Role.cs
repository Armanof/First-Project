using Bulky_Models.Base;
using Bulky_Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Models.Identity
{
    public class Role
        : BaseModel
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public override void StringNormalize()
        {
            Name = StringNormalization.NormalizeString(Name);
        }
    }

    public class RoleEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Role>
    {
        public RoleEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(300);

            builder.Property(x => x.IsDefault)
                .HasDefaultValueSql("0");
        }
    }
}

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
    public class UserRole
        : BaseModel
    {
        public Guid F_UserId { get; set; }
        public Guid F_RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        public override void StringNormalize()
        {
        }
    }

    public class UserRoleEntityTypeConfiguration : BaseModelEntityTypeConfiguration<UserRole>
    {
        public UserRoleEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.F_UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.F_RoleId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}

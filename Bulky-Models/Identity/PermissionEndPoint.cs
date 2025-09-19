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
    public class PermissionEndPoint
        : BaseModel
    {
        public Guid? F_PermissionId { get; set; }
        public Guid? F_EndPointID { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual EndPoint EndPoint { get; set; }



        public override void StringNormalize()
        {
        }
    }

    public class PermissionEndPointsEntityTypeConfiguration : BaseModelEntityTypeConfiguration<PermissionEndPoint>
    {
        public PermissionEndPointsEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<PermissionEndPoint> builder)
        {
            

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.PermissionEndPoints)
                .HasForeignKey(x => x.F_PermissionId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasOne(x => x.EndPoint)
                .WithMany(x => x.PermissionEndPoints)
                .HasForeignKey(x => x.F_EndPointID)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}

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

        public string EndPointName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }



        public Guid? F_PermissionId { get; set; }

        public virtual Permission Permission { get; set; }



        public override void StringNormalize()
        {
            StringNormalization.NormalizeString(EndPointName);
            StringNormalization.NormalizeString(ControllerName);
            StringNormalization.NormalizeString(ActionName);
            StringNormalization.NormalizeString(HttpMethod);
        }
    }

    public class PermissionEndPointsEntityTypeConfiguration : BaseModelEntityTypeConfiguration<PermissionEndPoint>
    {
        public PermissionEndPointsEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<PermissionEndPoint> builder)
        {
            builder.Property(x => x.EndPointName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ControllerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.ActionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.HttpMethod)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.PermissionEndPoints)
                .HasForeignKey(x => x.F_PermissionId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.SetNull);
        }
    }
}

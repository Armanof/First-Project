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
    public class EndPoint
        : BaseModel
    {
        public string EndPointName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }


        public virtual ICollection<PermissionEndPoint> PermissionEndPoints { get; set; }

        public override void StringNormalize()
        {
            EndPointName = StringNormalization.NormalizeString(EndPointName);
            ControllerName = StringNormalization.NormalizeString(ControllerName);
            ActionName = StringNormalization.NormalizeString(ActionName);
            HttpMethod = StringNormalization.NormalizeString(HttpMethod);
        }
    }

    public class EndPointEntityTypeConfiguration
        : BaseModelEntityTypeConfiguration<EndPoint>
    {
        public EndPointEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<EndPoint> builder)
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
        }
    }
}

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
    public class User
        : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string SaltPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SentCode { get; set; }
        public bool IsDeveloper { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }


        public override void StringNormalize()
        {
            Name = StringNormalization.NormalizeString(Name);
            PhoneNumber = StringNormalization.NormalizeString(PhoneNumber);
            Email = StringNormalization.NormalizeString(Email);
        }
    }

    public class UserEntityTypeConfiguration : BaseModelEntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(512)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.SaltPassword)
                .HasMaxLength(512)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.SentCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(x => x.IsDeveloper)
                .HasDefaultValueSql("0");
        }
    }
}

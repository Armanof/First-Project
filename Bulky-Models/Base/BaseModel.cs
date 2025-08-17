using Bulky_Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Models.Base
{
    public abstract class BaseModel:ISoftDelete
    {
        public Guid Id { get; set; }

        public string? InsertedIP { get; set; }
        public DateTime? InsertedDateTime { get; set; }
        public Guid? InsertedUserID { get; set; }

        public string? UpdatedIP { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public Guid? UpdatedUserID { get; set; }

        public bool IsDeleted { get; set; }

        public abstract void StringNormalize();
    }

    public abstract class BaseModelEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseModel
    {
        private readonly IHttpContextAccessor accessor;

        public BaseModelEntityTypeConfiguration(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }
        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");


            builder.Property(x => x.InsertedIP)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(x => x.UpdatedIP)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(x => x.InsertedDateTime)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.IsDeleted)
                .HasDefaultValueSql("0");

            builder.HasQueryFilter(x => !x.IsDeleted);

            ConfigureEntity(builder);
        }

    }
}

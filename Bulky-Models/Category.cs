using Bulky_Models.Base;
using Bulky_Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Models
{
    public class Category
        : BaseModel
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public override void StringNormalize()
        {
            Name = StringNormalization.NormalizeString(Name);
        }
    }

    public class CategoryEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Category>
    {
        public CategoryEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasData(new Category() { Id = Guid.Parse("33224A6D-AEFE-442E-8AFD-00335258C001"), Name = "Fantasy" });
            builder.HasData(new Category() { Id = Guid.Parse("7A687151-1B28-4217-B1DD-89BAC7F8BE86"), Name = "Sci-Fi" });
            builder.HasData(new Category() { Id = Guid.Parse("86E47E6E-03DB-4E6A-BEA3-A17CC6FA3408"), Name = "Romance" });
        }
    }
}

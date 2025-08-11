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

namespace Bulky_Models
{
    public class Product
        : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        public Guid? F_CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public override void StringNormalize()
        {
            Title = StringNormalization.NormalizeString(Title);
            Description = StringNormalization.NormalizeString(Description);
            ISBN = StringNormalization.NormalizeString(ISBN);
            Author = StringNormalization.NormalizeString(Author);
        }
    }

    public class ProductEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Product>
    {
        public ProductEntityTypeConfiguration(IHttpContextAccessor accessor) : base(accessor)
        {
        }

        protected override void ConfigureEntity(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(x => x.ISBN)
                    .HasMaxLength(100);

            builder.Property(x => x.Author)
                    .HasMaxLength(300);

            builder.Property(x => x.ImageUrl)
                    .HasMaxLength(500)
                    .IsRequired(false);

            builder.HasData(
                new List<Product>
                {
                    new Product { Id = Guid.Parse("E82A1C1B-0F1C-4222-94FB-50F1B5B46E12"), Title = "C# Programming", Description = "Advanced C# techniques", ISBN = "978-1234567890", Author = "John Doe", Price = 49.99 },
                    new Product { Id = Guid.Parse("4C53D0B3-09A9-42C4-A569-74BDCF7D7640"), Title = "ASP.NET Core Guide", Description = "Building scalable web applications", ISBN = "978-0987654321", Author = "Jane Smith", Price = 69.99 },
                    new Product { Id = Guid.Parse("E57D5E7F-0148-4F10-81E2-8051FAEBFFA8"), Title = "Introduction to Microservices", Description = "Modern distributed system design", ISBN = "978-5678901234", Author = "Michael Chen", Price = 79.99 },
                    new Product { Id = Guid.Parse("3BD11B1F-D805-4564-961A-F5A69EBB490B"), Title = "Clean Code", Description = "Principles of software craftsmanship", ISBN = "978-3216549870", Author = "Robert Martin", Price = 42.99 },
                    new Product { Id = Guid.Parse("B824129F-3925-4DE0-A014-9EA5535591D8"), Title = "Machine Learning with Python", Description = "Hands-on ML algorithms and data science", ISBN = "978-6789012345", Author = "Sarah Lee", Price = 89.99 }
                });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.F_CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

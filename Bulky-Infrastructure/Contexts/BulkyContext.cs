using Bulky_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Infrastructure.Contexts
{
    public class BulkyContext :  DbContext
    {
        private readonly IHttpContextAccessor accessor;

        public BulkyContext(DbContextOptions<BulkyContext> options, IHttpContextAccessor accessor) : base(options)
        {
            this.accessor = accessor;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration(accessor));
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration(accessor));
        }
    }
}

using Bulky_Models;
using Bulky_Models.Identity;
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

        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<PermissionEndPoint> PermissionEndPoints { get; set; }



        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Identity Models

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration(accessor));
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration(accessor));
            modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration(accessor));
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration(accessor));
            modelBuilder.ApplyConfiguration(new RolePermissionEntityTypeConfiiguration(accessor));
            modelBuilder.ApplyConfiguration(new PermissionEndPointsEntityTypeConfiguration(accessor));

            #endregion


            #region Business Models

            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration(accessor));
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration(accessor));

            #endregion
        }
    }
}

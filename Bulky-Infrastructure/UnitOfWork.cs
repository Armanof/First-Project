using Bulky_Infrastructure.Contexts;
using Bulky_Infrastructure.Interfaces;
using Bulky_Infrastructure.Repositories;
using Bulky_Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Infrastructure
{
    public class UnitOfWork
        : IUnitOfWork
    {
        private readonly BulkyContext db;
        private readonly IHttpContextAccessor context;
        private Dictionary<Type, object> Repositories;

        public UnitOfWork(BulkyContext db,IHttpContextAccessor context)
        {
            this.db = db;
            this.context = context;
            Repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> Repository<T>() where T : BaseModel, new()
        {
            object rep;
            if (!Repositories.TryGetValue(typeof(IRepository<T>), out rep))
            {
                rep = new Repository<T>(db);
                Repositories.Add(typeof(IRepository<T>), rep);
            }

            return (IRepository<T>)rep;
        }

        private void BeforeSaveChange()
        {
            var userIdClaim = context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid? userId = null;
            if (Guid.TryParse(userIdClaim, out var parsedGuid))
            {
                userId = parsedGuid;
            }

            var remoteIpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            foreach (var entry in db.ChangeTracker.Entries<BaseModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.InsertedUserID = userId;
                        entry.Entity.InsertedIP = remoteIpAddress;
                        entry.Entity.InsertedDateTime = DateTime.UtcNow;
                        db.Entry(entry.Entity).Property(x => x.UpdatedDateTime).IsModified = false;
                        db.Entry(entry.Entity).Property(x => x.UpdatedIP).IsModified = false;
                        db.Entry(entry.Entity).Property(x => x.UpdatedUserID).IsModified = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedUserID = userId;
                        entry.Entity.UpdatedIP = remoteIpAddress;
                        entry.Entity.UpdatedDateTime = DateTime.UtcNow;
                        db.Entry(entry.Entity).Property(x => x.InsertedDateTime).IsModified = false;
                        db.Entry(entry.Entity).Property(x => x.InsertedIP).IsModified = false;
                        db.Entry(entry.Entity).Property(x => x.InsertedUserID).IsModified = false;
                        break;
                }
            }
        }

        public async Task SaveChangesAsync()
        {
            BeforeSaveChange();
            await db.SaveChangesAsync();
        }
    }
}

namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class PermissionsRepository : IRepository<Permission, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PermissionsRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Permission FindById(int id)
        {
            return this.serviceDbContext.Permissions.SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<Permission> FindAll()
        {
            return this.serviceDbContext.Permissions;
        }

        public void Add(Permission permission)
        {
            this.serviceDbContext.Permissions.Add(permission);
        }

        public void Remove(Permission permission)
        {
            this.serviceDbContext.Permissions.Remove(permission);
        }

        public Permission FindBy(Expression<Func<Permission, bool>> expression)
        {
            return this.serviceDbContext.Permissions.SingleOrDefault(expression);
        }

        public IQueryable<Permission> FilterBy(Expression<Func<Permission, bool>> expression)
        {
            var individual = this.serviceDbContext.Permissions.Where(p => p is IndividualPermission).Include(x => x.Privilege).ToList();
            var group = this.serviceDbContext.Permissions.Where(p => p is GroupPermission).Include(x => ((GroupPermission)x).GranteeGroup).Include(x => x.Privilege).ToList();
            var all = group.Concat(individual).AsQueryable().Where(expression);
            return all;
        }
    }
}

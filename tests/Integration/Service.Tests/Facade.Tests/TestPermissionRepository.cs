namespace Linn.Authorisation.Service.Tests.Facade.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Domain;
    using Domain.Groups;
    using Domain.Permissions;

    using Linn.Common.Persistence;

    public class TestPermissionRepository : IRepository<Permission, int>
    {
        public IEnumerable<Permission> GetIndividualPermissions(string who)
        {
            return TestDbContext.Permissions.Where(p => p is IndividualPermission && (((IndividualPermission) p).GranteeUri == who));
        }

        public IEnumerable<Permission> GetGroupsPermissions(IEnumerable<Group> groups)
        {
            return TestDbContext.Permissions.Where(p =>
                p is GroupPermission && groups.Contains(((GroupPermission) p).GranteeGroup));
        }

        public Permission FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Permission> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Permission entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Permission entity)
        {
            throw new NotImplementedException();
        }

        public Permission FindBy(Expression<Func<Permission, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Permission> FilterBy(Expression<Func<Permission, bool>> expression)
        {
            return TestDbContext.Permissions.AsQueryable().Where(expression);
        }
    }
}

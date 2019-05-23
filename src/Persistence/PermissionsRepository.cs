namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Persistence;

    public class PermissionsRepository : IRepository<Permission, int>
    {
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
            throw new NotImplementedException();
        }
    }
}

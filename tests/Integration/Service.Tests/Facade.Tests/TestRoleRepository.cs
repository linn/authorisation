namespace Linn.Authorisation.Integration.Tests.Facade.Tests
{
    using System;
    using System.Linq;
    using Common.Persistence;

    using System.Linq.Expressions;
    using Domain;

    public class TestRoleRepository : IRepository<Role, int>
    {
        public Role FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Role entity)
        {
            throw new NotImplementedException();
        }

        public Role FindBy(Expression<Func<Role, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> FilterBy(Expression<Func<Role, bool>> expression)
        {
            return TestDbContext.Roles.AsQueryable().Where(expression);
        }
    }
}

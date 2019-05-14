namespace Linn.Authorisation.Service.Tests.Facade.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common.Persistence;
    using Domain;

    public class TestRoleRepository : IRepository<Domain.Role, int>
    {
        public Domain.Role FindById(int key)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<Domain.Role> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.Role entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Domain.Role entity)
        {
            throw new NotImplementedException();
        }

        public Domain.Role FindBy(System.Linq.Expressions.Expression<Func<Domain.Role, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public System.Linq.IQueryable<Domain.Role> FilterBy(System.Linq.Expressions.Expression<Func<Domain.Role, bool>> expression)
        {
            return TestDbContext.Roles.AsQueryable().Where(expression);
        }
    }
}

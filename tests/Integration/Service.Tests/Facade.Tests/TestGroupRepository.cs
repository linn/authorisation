namespace Linn.Authorisation.Service.Tests.Facade.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Persistence;
    using Domain.Groups;

    public class TestGroupRepository : IRepository<Group, int>
    {
        public Group FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Group> FindAll()
        {
            return TestDbContext.Groups.AsQueryable();
        }

        public void Add(Group entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Group entity)
        {
            throw new NotImplementedException();
        }

        public Group FindBy(Expression<Func<Group, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Group> FilterBy(Expression<Func<Group, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
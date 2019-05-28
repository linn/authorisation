namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Persistence;

    public class GroupRepository : IRepository<Group, int>
    {
        public Group FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Group> FindAll()
        {
            throw new NotImplementedException();
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
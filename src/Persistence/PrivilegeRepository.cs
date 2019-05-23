namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Common.Persistence;

    public class PrivilegeRepository : IRepository<Privilege, int>
    {
        public Privilege FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Privilege FindById(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Privilege> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Privilege entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Privilege entity)
        {
            throw new NotImplementedException();
        }

        public Privilege FindBy(Expression<Func<Privilege, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Privilege> FilterBy(Expression<Func<Privilege, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;
    using Linn.Common.Persistence;

    public class PrivilegeRepository : IRepository<Privilege, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public PrivilegeRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Privilege FindByName(string name)
        {
            return this.serviceDbContext.Privileges.SingleOrDefault(p => p.Name == name);
        }

        public Privilege FindById(int id)
        {
            return this.serviceDbContext.Privileges.SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<Privilege> FindAll()
        {
            return this.serviceDbContext.Privileges.OrderBy(x => x.Name);
        }

        public void Add(Privilege privilege)
        {
            this.serviceDbContext.Privileges.Add(privilege);
        }

        public void Remove(Privilege privilege)
        {
            this.serviceDbContext.Privileges.Remove(privilege);
        }

        public Privilege FindBy(Expression<Func<Privilege, bool>> expression)
        {
            return this.serviceDbContext.Privileges.SingleOrDefault(expression);
        }

        public IQueryable<Privilege> FilterBy(Expression<Func<Privilege, bool>> expression)
        {
            return this.serviceDbContext.Privileges.Where(expression);
        }
    }
}
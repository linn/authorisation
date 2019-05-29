﻿namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Persistence;

    public class GroupRepository : IRepository<Group, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public GroupRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Group FindById(int id)
        {
            return this.serviceDbContext.Groups.SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<Group> FindAll()
        {
            return this.serviceDbContext.Groups;
        }

        public void Add(Group group)
        {
            this.serviceDbContext.Groups.Add(group);
        }

        public void Remove(Group group)
        {
            this.serviceDbContext.Groups.Add(group);
        }

        public Group FindBy(Expression<Func<Group, bool>> expression)
        {
            return this.serviceDbContext.Groups.SingleOrDefault(expression);
        }

        public IQueryable<Group> FilterBy(Expression<Func<Group, bool>> expression)
        {
            return this.serviceDbContext.Groups.Where(expression);
        }
    }
}
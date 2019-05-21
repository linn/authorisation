namespace Linn.Authorisation.Service.Tests.Facade.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Common.Persistence;
    using Domain;
    using Domain.Groups;

    using Linn.Authorisation.Domain.Repositories;

    public class TestGroupRepository : IGroupRepository
    {
        public IEnumerable<Group> GetGroups()
        {
            return TestDbContext.Groups.AsQueryable();
        }
    }
}
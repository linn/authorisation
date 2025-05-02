namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;

    public class MemberRepository : EntityFrameworkRepository<Member, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MemberRepository(ServiceDbContext serviceDbContext) : base(serviceDbContext.Members)
        {
            this.serviceDbContext = serviceDbContext;
        }
    }
}

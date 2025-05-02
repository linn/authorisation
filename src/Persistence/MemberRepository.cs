namespace Linn.Authorisation.Persistence
{
    using Linn.Authorisation.Domain.Groups;
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

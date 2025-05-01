namespace Linn.Authorisation.Persistence
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Persistence;

    public class MemberRepository : IRepository<Member, int>
    {
        private readonly ServiceDbContext serviceDbContext;

        public MemberRepository(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }

        public Member FindById(int id)
        {
            return this.serviceDbContext.Members.SingleOrDefault(p => p.Id == id);
        }

        public IQueryable<Member> FindAll()
        {
            return this.serviceDbContext.Members.OrderBy(p => p.Id);
        }

        public void Add(Member privilege)
        {
            this.serviceDbContext.Members.Add(privilege);
        }

        public void Remove(Member privilege)
        {
            this.serviceDbContext.Members.Remove(privilege);
        }

        public Member FindBy(Expression<Func<Member, bool>> expression)
        {
            return this.serviceDbContext.Members.SingleOrDefault(expression);
        }

        public IQueryable<Member> FilterBy(Expression<Func<Member, bool>> expression)
        {
            return this.serviceDbContext.Members.Where(expression);
        }
    }
}

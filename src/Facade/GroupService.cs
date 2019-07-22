namespace Linn.Authorisation.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Domain.Exceptions;
    using Domain.Groups;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using PagedList.Core;

    public class GroupService : FacadeService<Group, int, GroupResource, GroupResource>, IGroupService
    {
        private readonly IRepository<Group, int> groupRepository;
        private readonly IRepository<Member, int> memberRepository;


        private readonly ITransactionManager transactionManager;

        public GroupService(IRepository<Group, int> groupRepository, IRepository<Member, int> memberRepository, ITransactionManager transactionManager) 
            : base(groupRepository, transactionManager)
        {
            this.groupRepository = groupRepository;
            this.transactionManager = transactionManager;
            this.memberRepository = memberRepository;
        }

        protected override Group CreateFromResource(GroupResource resource)
        {
            return new Group(resource.Name, resource.Active);
        }

        protected override void UpdateFromResource(Group group, GroupResource updateResource)
        {
            group.Name = updateResource.Name;
            group.Active = updateResource.Active;
        }

        protected override Expression<Func<Group, bool>> SearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public IResult<Group> AddGroupMember(int groupId, GroupMemberResource resource)
        {
            if ((string.IsNullOrEmpty(resource.MemberUri)) && (string.IsNullOrEmpty(resource.GroupName)))
            {
                return new BadRequestResult<Group>("No member or group supplied");
            }

            var group = this.groupRepository.FindById(groupId);
            if (group == null)
            {
                return new NotFoundResult<Group>($"group {groupId} not found");
            }

            if (!string.IsNullOrEmpty(resource.MemberUri))
            {
                try
                {
                    group.AddIndividualMember(resource.MemberUri, resource.AddedByUri);
                }
                catch (MemberAlreadyInGroupException e)
                {
                    return new BadRequestResult<Group>(e.Message);
                }
            }

            if (!string.IsNullOrEmpty(resource.GroupName))
            {
                var subGroup = this.groupRepository.FindBy(g => g.Name == resource.GroupName);
                if (subGroup == null)
                {
                    return new BadRequestResult<Group>($"Group {resource.GroupName} does not exist");
                }

                group.AddGroupMember(subGroup, resource.AddedByUri);
            }

            this.transactionManager.Commit();

            return new SuccessResult<Group>(group);
        }

        public IResult<Group> RemoveGroupMember(int groupId, int memberId)
        {
            var group = this.groupRepository.FindById(groupId);
            if (group == null)
            {
                return new NotFoundResult<Group>($"group {groupId} not found");
            }

            var member = group.Members.SingleOrDefault(m => m.Id == memberId);
            if (member == null)
            {
                return new NotFoundResult<Group>($"member {memberId} not found on group {group.Name}");
            }

            group.RemoveMember(member);
            this.transactionManager.Commit();

            return new SuccessResult<Group>(group);
        }

        public IEnumerable<Member> GetImmediateMembers(int groupId)
        {
            var group = this.groupRepository.FindById(groupId);
            if (group == null)
            {
                return new NotFoundResult<Group>($"group {groupId} not found");
            }

            var members = group.Members


            //var members = this.memberRepository.FilterBy(m => m is IndividualMember && ((IndividualMember)m. == groupId)
            //    .Select(p => p.Privilege).ToList();

            return privileges.Where(p => p.Active).Distinct();
        }
    }
}

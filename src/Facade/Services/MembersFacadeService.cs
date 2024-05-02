using Linn.Authorisation.Domain.Permissions;
using Linn.Authorisation.Domain.Services;
using Linn.Authorisation.Domain;
using Linn.Common.Facade;
using Linn.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linn.Authorisation.Facade.Services
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;

    public class MembersFacadeService
    {
        private readonly IBuilder<Member> resourceBuilder;

        private readonly IRepository<Member, int> memberRepository;

        private readonly IRepository<Group, int> groupRepository;

        private readonly ITransactionManager transactionManager;

        public MembersFacadeService(
            IPermissionService permissionService,
            IBuilder<Member> resourceBuilder,
            IRepository<Member, int> memberRepository,
            IRepository<Group, int> groupRepository,
            ITransactionManager transactionManager)
        {
            this.resourceBuilder = resourceBuilder;
            this.memberRepository = memberRepository;
            this.transactionManager = transactionManager;
            this.groupRepository = groupRepository;
        }
        public IResult<IndividualMember> AddIndividualMember(MemberResource memberResource, string employeeUri)
        {
            var group = this.groupRepository.FindById((int)memberResource.MemberUri);

            var newMember = new IndividualMember(
                employeeUri, memberResource.MemberUri);

            var members = this.memberRepository.FilterBy(m => m is IndividualMember);

            var individualMembers = members.Select(m => (IndividualMember)m);

            if (newMember.CheckUnique(individualMembers))
            {
                this.memberRepository.Add(newMember);

                this.transactionManager.Commit();

                var result = new IndividualMember
                                 {
                                     DateAdded = newMember.DateAdded,
                                     AddedByUri = newMember.AddedByUri,
                                     MemberUri = newMember.MemberUri,
                                     Id = newMember.Id
                                 };

                return new CreatedResult<IndividualMember>(result);
            }

            return new BadRequestResult<IndividualMember>("Grantee already has privilege");
        }

        public IResult<GroupMember> AddGroupPermission(MemberResource memberResource, string groupId)
        {
            var privilege = this.groupRepository.FindById(memberResource.MemberUri);

            if (memberResource.Group != null)
            {
                var group = this.groupRepository.FindById((int)memberResource.Group);

                var newGroupMember = new GroupMember(
                    group, memberResource.MemberUri);

                var members = this.memberRepository.FilterBy(m => m is GroupMember);

                var groupMembers = members.Select(m => (GroupMember)m);

                if (newGroupMember.CheckUnique(groupMembers))
                {
                    this.memberRepository.Add(newGroupMember);

                    this.transactionManager.Commit();

                    var result = new GroupMember
                                     {
                                         Group = newGroupMember.Group;
                                         AddedByUri = newGroupMember.addedByUri;
                                         DateAdded = DateTime.UtcNow;DateGranted = permission.DateGranted.ToString("o"),
                                     };
                    return new CreatedResult<GroupMember>(result);
                }
            }

            return new BadRequestResult<GroupMember>("Grantee already has privilege");
        }
}

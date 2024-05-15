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
    using Amazon.Auth.AccessControlPolicy;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Resources;

    public class MembersFacadeService : IMembersFacadeService
    {
        private readonly IBuilder<Member> resourceBuilder;

        private readonly IRepository<Group, int> groupRepository;

        private readonly ITransactionManager transactionManager;

        public MembersFacadeService(
            IBuilder<Member> resourceBuilder,
            IRepository<Group, int> groupRepository,
            ITransactionManager transactionManager)
        {
            this.resourceBuilder = resourceBuilder;
            this.transactionManager = transactionManager;
            this.groupRepository = groupRepository;
        }

        public IResult<MemberResource> AddIndividualMember(MemberResource memberResource, string employeeUri)
        {
            var group = this.groupRepository.FindById((int)memberResource.GroupId);

            var resource = this.resourceBuilder;

            group.AddIndividualMember(memberResource.MemberUri, employeeUri);

            this.transactionManager.Commit();

            try
            {
                return new CreatedResult<MemberResource>(new MemberResource { MemberUri = memberResource.MemberUri });
            }
            catch (MemberAlreadyInGroupException)
            {
                throw new MemberAlreadyInGroupException($"{memberResource.MemberUri} already exists in group");
            }
            catch (Exception ex)
            {
                return new BadRequestResult<MemberResource>(ex.Message);
            }
        }
    }
}

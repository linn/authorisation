using System.Collections.Generic;

namespace Linn.Authorisation.Facade.Services
{
    using System;

    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class MembersFacadeService : IMembersFacadeService
    {
        private readonly IRepository<Group, int> groupRepository;

        private readonly ITransactionManager transactionManager;

        public MembersFacadeService(
            IRepository<Group, int> groupRepository,
            ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
            this.groupRepository = groupRepository;
        }

        public IResult<MemberResource> AddIndividualMember(MemberResource memberResource, string employeeUri, IEnumerable<string> userPrivileges = null)
        {
            var group = this.groupRepository.FindById((int)memberResource.GroupId);

            try
            {
                group.AddIndividualMember(memberResource.MemberUri, employeeUri);
                this.transactionManager.Commit();
                return new CreatedResult<MemberResource>(new MemberResource { MemberUri = memberResource.MemberUri });
            }
            catch (MemberAlreadyInGroupException ex)
            {
                return new BadRequestResult<MemberResource>(ex.Message);
            }
            catch (Exception ex)
            {
                return new BadRequestResult<MemberResource>(ex.Message);
            }
        }
    }
}

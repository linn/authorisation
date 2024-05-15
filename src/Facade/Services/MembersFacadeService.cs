


namespace Linn.Authorisation.Facade.Services
{
    using Linn.Common.Facade;
    using Linn.Common.Persistence;
    using System;

    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;

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

        public IResult<MemberResource> AddIndividualMember(MemberResource memberResource, string employeeUri)
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

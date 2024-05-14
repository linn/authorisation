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
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Resources;

    public class MembersFacadeService : IMembersFacadeService
    {
        private readonly MemberResourceBuilder resourceBuilder;

        private readonly IRepository<Group, int> groupRepository;

        private readonly ITransactionManager transactionManager;

        public MembersFacadeService(
            MemberResourceBuilder resourceBuilder,
            IRepository<Group, int> groupRepository,
            ITransactionManager transactionManager)
        {
            this.resourceBuilder = resourceBuilder;
            this.transactionManager = transactionManager;
            this.groupRepository = groupRepository;
        }

        public IResult<MemberResource> AddIndividualMember(MemberResource memberResource, string employeeUri)
        {
            var group = this.groupRepository.FindById(memberResource.GroupId.Value);

            group.AddIndividualMember(memberResource.MemberUri, employeeUri);

            this.transactionManager.Commit();


            // TODO implement try catch and return a bad request if bad action caught
            // Complete

            try
            {
                return new CreatedResult<MemberResource>(new MemberResource { MemberUri = memberResource.MemberUri });
            }
            catch (MemberAlreadyInGroupException)
            {
                throw new MemberAlreadyInGroupException($"{memberResource.MemberUri} already exists in group");
            }
            

            //TODO use the resource builder to construct a resource


        }
    }
}

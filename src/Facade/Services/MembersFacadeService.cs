﻿namespace Linn.Authorisation.Facade.Services
{
    using System;
    using System.Collections.Generic;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class MembersFacadeService : IMembersFacadeService
    {
        private readonly IRepository<Group, int> groupRepository;

        private readonly IRepository<Member, int> memberRepository;

        private readonly ITransactionManager transactionManager;

        private readonly IAuthorisationService authService;

        public MembersFacadeService(
            IRepository<Group, int> groupRepository,
            IRepository<Member, int> memberRepository,
            ITransactionManager transactionManager,
            IAuthorisationService authService)
        {
            this.transactionManager = transactionManager;
            this.groupRepository = groupRepository;
            this.memberRepository = memberRepository;
            this.authService = authService;
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

        public IResult<MemberResource> DeleteMember(int memberId, IEnumerable<string> userPrivileges = null)
        {
            if (!this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAuthManager, userPrivileges))
            {
                throw new UnauthorisedActionException("You do not have permission to delete a member");
            }

            var member = this.memberRepository.FindById(memberId);

            this.memberRepository.Remove(member);

            this.transactionManager.Commit();

            var result = new MemberResource { GroupId = null, MemberUri = null };

            return new SuccessResult<MemberResource>(result);
        }
    }
}

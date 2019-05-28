namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Facade;

    public class AuthorisationService : IAuthorisationService
    {
        private readonly IPrivilegeService privilegeService;

        public AuthorisationService(IPrivilegeService privilegeService)
        {
            this.privilegeService = privilegeService;
        }

        public IResult<IEnumerable<Privilege>> GetPrivilegesForMember(string who)
        {
            return new SuccessResult<IEnumerable<Privilege>>(this.privilegeService.GetPrivileges(who));
        }
    }
}

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
            var result = this.privilegeService.GetPrivileges(who);

           if(result != null) return new SuccessResult<IEnumerable<Privilege>>(result);

           return new BadRequestResult<IEnumerable<Privilege>>("No who provided");
        }
    }
}

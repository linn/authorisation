namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repositories;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class AuthorisationService : IAuthorisationService
    {
        private readonly IPrivilegeService privilegeService;

        public AuthorisationService(IPrivilegeService privilegeService)
        {
            this.privilegeService = privilegeService;
        }

        public IResult<IEnumerable<Privilege>> GetPrivileges(string who)
        {
            return new SuccessResult<IEnumerable<Privilege>>(this.privilegeService.GetPrivileges(who));
        }
    }
}

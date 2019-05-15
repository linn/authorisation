namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class AuthorisationService : IAuthorisationService
    {
        private readonly IRepository<Role, int> roleRepository;

        public AuthorisationService(IRepository<Role, int> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IResult<IEnumerable<Claim>> GetClaims(string who)
        {
            var roles = this.roleRepository.FilterBy(role => role.Members.Any(uri => uri.Equals(who)));
            var claims = roles.SelectMany(role => role.Claims).ToList();
            return new SuccessResult<IEnumerable<Claim>>(claims);
        }
    }
}

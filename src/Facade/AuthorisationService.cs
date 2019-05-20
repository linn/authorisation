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

        public IResult<IEnumerable<Permission>> GetPermissions(string who)
        {
            var roles = this.roleRepository.FilterBy(role => role.Members.Any(uri => uri.Equals(who)));
            var Permissions = roles.SelectMany(role => role.Permissions).ToList();
            return new SuccessResult<IEnumerable<Permission>>(Permissions);
        }
    }
}

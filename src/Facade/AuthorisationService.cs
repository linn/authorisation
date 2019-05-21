namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repositories;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

    public class AuthorisationService : IAuthorisationService
    {
        private readonly IPermissionRepository permissionRepository;

        private readonly IRepository<Group, int> groupRepository;

        public AuthorisationService(IPermissionRepository permissionRepository, IRepository<Group, int> groupRepository)
        {
            this.permissionRepository = permissionRepository;
            this.groupRepository = groupRepository;
        }

        public IResult<IEnumerable<Privilege>> GetPrivileges(string who)
        {
            var privileges = this.permissionRepository.GetIndividualPermissions(who)
                .Select(p => p.Privilege).ToList();

            var groups = this.groupRepository.FindAll().Where(g => g.IsMemberOf(who));
            if (!groups.Any())
            {
                return new SuccessResult<IEnumerable<Privilege>>(privileges.Where(p => p.Active).Distinct());
            }

            var groupPermissions = this.permissionRepository.GetGroupsPermissions(groups);
            privileges.AddRange(groupPermissions.Select(p => p.Privilege));

            return new SuccessResult<IEnumerable<Privilege>>(privileges.Where(p => p.Active).Distinct());
        }
    }
}

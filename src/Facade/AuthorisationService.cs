namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repositories;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Common.Facade;

    public class AuthorisationService : IAuthorisationService
    {
        private readonly IPermissionRepository permissionRepository;

        private readonly IGroupService groupService;

        public AuthorisationService(IPermissionRepository permissionRepository, IGroupService groupService)
        {
            this.permissionRepository = permissionRepository;
            this.groupService = groupService;
        }

        public IResult<IEnumerable<Privilege>> GetPrivileges(string who)
        {
            var privileges = this.permissionRepository.GetIndividualPermissions(who)
                .Select(p => p.Privilege).ToList();

            var groups = this.groupService.GetGroups(who).ToList();
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

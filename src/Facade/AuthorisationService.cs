namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repositories;
    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;
    using Linn.Common.Persistence;

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

            var groups = this.groupService.GetGroups(who);
            if (groups.Any())
            {
                var groupPrivileges = this.permissionRepository.GetGroupsPermissions(groups)
                    .Select(p => p.Privilege).ToList();
                privileges.AddRange(groupPrivileges);
            }

            return new SuccessResult<IEnumerable<Privilege>>(privileges.Distinct());
        }
    }
}

using Linn.Authorisation.Domain.Permissions;

namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class GroupResourceBuilder : IBuilder<Group>
    {
        private readonly MemberResourceBuilder memberResourceBuilder = new();

        private readonly PermissionResourceBuilder permissionResourceBuilder = new();

        public object Build(Group model, IEnumerable<string> claims)
        {
            var membersResources = model.Members?
                .Select(member => (MemberResource)this.memberResourceBuilder.Build(member, claims)).ToList();

            var permissionResources = model.Permissions?
                .Select(permission => (PermissionResource)this.permissionResourceBuilder.Build(permission, claims)).ToList();

            return new GroupResource
            {
                Active = model.Active,
                Name = model.Name,
                Id = model.Id,
                Members = membersResources,
                Permission = permissionResources,
            };
        }

        public string GetLocation(Group model)
        {
            throw new System.NotImplementedException();
        }

        object IBuilder<Group>.Build(Group model, IEnumerable<string> claims) => this.Build(model, claims);
    }
}
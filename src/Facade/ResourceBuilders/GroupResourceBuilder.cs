namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class GroupResourceBuilder : IBuilder<Group>
    {
        private readonly MemberResourceBuilder memberResourceBuilder = new ();

        private readonly PermissionResourceBuilder permissionResourceBuilder = new ();

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
                Permissions = permissionResources,
                Links = this.BuildLinks(model).ToArray()
            };
        }

        public string GetLocation(Group model)
        {
            return $"/authorisation/groups/{model.Id}";
        }

        object IBuilder<Group>.Build(Group model, IEnumerable<string> claims) => this.Build(model, claims);

        private IEnumerable<LinkResource> BuildLinks(Group group)
        {
            yield return new LinkResource
                             {
                                 Rel = "self",
                                 Href = this.GetLocation(group)
                             };
        }
    }
}

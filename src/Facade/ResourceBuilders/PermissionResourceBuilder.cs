namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PermissionResourceBuilder : IBuilder<Permission>
    {
        public object Build(Permission model, IEnumerable<string> claims)
        {
            if (model is IndividualPermission)
            {
                return new PermissionResource
                {
                    GranteeUri = ((IndividualPermission)model).GranteeUri,
                    Privilege = model.Privilege.Name,
                    PrivilegeId = model.Privilege.Id,
                    Links = this.BuildLinks(model).ToArray(),
                    Id = model.Id
                };
            }

            return new PermissionResource
            {
                Privilege = model.Privilege.Name,
                PrivilegeId = model.Privilege.Id,
                Links = this.BuildLinks(model).ToArray(),
                Id = model.Id,
                GranteeGroupId = ((GroupPermission)model).GranteeGroup.Id,
                GroupName = ((GroupPermission)model).GranteeGroup.Name
            };
        }

        public string GetLocation(Permission model)
        {
            return $"/authorisation/permissions/{model.Id}";
        }

        object IBuilder<Permission>.Build(Permission model, IEnumerable<string> claims) => this.Build(model, claims);

        private IEnumerable<LinkResource> BuildLinks(Permission permission)
        {
            yield return new LinkResource { Rel = "self", Href = $"/permissions/{permission.Id}" };
        }
    }
}
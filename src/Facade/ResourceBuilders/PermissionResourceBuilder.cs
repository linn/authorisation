namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PermissionResourceBuilder : IResourceBuilder<Permission>
    {
        public PermissionResource Build(Permission permission)
        {
            if (permission is IndividualPermission individualPermission)
            {
                return new PermissionResource
                           {
                               GrantedByUri = individualPermission.GrantedByUri,
                               GranteeUri = individualPermission.GranteeUri,
                               Privilege = individualPermission.Privilege.Name,
                               DateGranted = individualPermission.DateGranted.ToString("o")
                };
            }

            var groupPermission = (GroupPermission)permission;
            return new PermissionResource
                       {
                           GrantedByUri = groupPermission.GrantedByUri,
                           GroupName = groupPermission.GranteeGroup.Name,
                           Privilege = groupPermission.Privilege.Name,
                           DateGranted = groupPermission.DateGranted.ToString("o"),
                           GranteeGroupId = groupPermission.GranteeGroup.Id.ToString(),
                           Links = this.BuildLinks(groupPermission).ToArray()
            };
        }

        object IResourceBuilder<Permission>.Build(Permission p) => this.Build(p);

        public string GetLocation(Permission model)
        {
            return $"/authorisation/permissions/{model.Id}";
        }

        public IEnumerable<LinkResource> BuildLinks(GroupPermission permission)
        {
            yield return new LinkResource
                             {
                                 Rel = "group",
                                 Href = $"/authorisation/groups/{permission.GranteeGroup.Id}"
                             };
        }
    }
}
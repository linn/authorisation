namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

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
                               DateGranted = individualPermission.DateGranted.ToString("ddd, dd MMMM yyyy hh:mm tt")
                };
            }

            var groupPermission = (GroupPermission)permission;
            return new PermissionResource
                       {
                           GrantedByUri = groupPermission.GrantedByUri,
                           GroupName = groupPermission.GranteeGroup.Name,
                           Privilege = groupPermission.Privilege.Name,
                           DateGranted = groupPermission.DateGranted.ToString("ddd, dd MMMM yyyy hh:mm tt")
            };
        }

        object IResourceBuilder<Permission>.Build(Permission p) => this.Build(p);

        public string GetLocation(Permission model)
        {
            return $"/authorisation/permissions/{model.Id}";
        }
    }
}
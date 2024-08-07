namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PermissionResourceBuilder : IResourceBuilder<Permission>
    {
        public PermissionResource Build(Permission model)
        {
            return new PermissionResource
                             {
                                 Privilege = model.Privilege.Name,
                                 PrivilegeId = model.Privilege.Id,
                                 Links = this.BuildLinks(model).ToArray()
                             };
        }
        
        public string GetLocation(Permission model)
        {
            return $"/authorisation/permissions/{model.Id}";
        }

        object IResourceBuilder<Permission>.Build(Permission model) => this.Build(model);

        private IEnumerable<LinkResource> BuildLinks(Permission permission)
        {
            yield return new LinkResource
                             {
                                 Rel = "self",
                                 Href = $"/permissions/{permission.Id}"
                             };
        }
    }
}

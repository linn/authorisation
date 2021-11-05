namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class PermissionsResourceBuilder : IResourceBuilder<IEnumerable<Permission>>
    {
        public IEnumerable<PermissionResource> Build(IEnumerable<Permission> permissions)
        {
            var resourceBuilder = new PermissionResourceBuilder();
            var permissionList = new List<PermissionResource>();

            foreach(var permission in permissions) {
                permissionList.Add(resourceBuilder.Build(permission));
            }

            return permissionList;
        }

        object IResourceBuilder<IEnumerable<Permission>>.Build(IEnumerable<Permission> permissions) => this.Build(permissions);

        public string GetLocation(IEnumerable<Permission> model)
        {
            throw new NotImplementedException();
            //return $"/authorisation/groups/{model.Id}/permissions";
        }
    }
}
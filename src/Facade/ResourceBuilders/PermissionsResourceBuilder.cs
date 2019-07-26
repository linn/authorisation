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
            var permissionList = new List<PermissionResource>();

            foreach(var permission in permissions) {
                if (permission is IndividualPermission individualPermission)
                {
                    permissionList.Add(new PermissionResource
                    {
                        GrantedByUri = individualPermission.GrantedByUri,
                        GranteeUri = individualPermission.GranteeUri,
                        Privilege = individualPermission.Privilege.Name,
                        DateGranted = individualPermission.DateGranted.ToString("ddd, dd MMMM yyyy hh:mm tt")
                    });
                }
                else if (permission is GroupPermission)
                {
                    var groupPermission = (GroupPermission)permission;

                    permissionList.Add(
                        new PermissionResource
                            {
                                GrantedByUri = groupPermission.GrantedByUri,
                                GroupName = groupPermission.GranteeGroup.Name,
                                Privilege = groupPermission.Privilege.Name,
                                DateGranted = groupPermission.DateGranted.ToString("ddd, dd MMMM yyyy hh:mm tt")
                            });
                }
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
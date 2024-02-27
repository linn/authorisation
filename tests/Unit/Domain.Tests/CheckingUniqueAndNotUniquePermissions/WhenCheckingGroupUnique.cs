using Linn.Authorisation.Domain.Tests.PermissionServiceTests;

namespace Linn.Authorisation.Domain.Tests.CheckingUniqueAndNotUniquePermissions
{
    using FluentAssertions;
    using Linn.Authorisation.Domain.Permissions;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain.Groups;

    public class WhenCheckingGroupUnique : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";
        private readonly string privilegeName2 = "do.admin.stuuuff 2";
        private readonly string privilegeName3 = "do.admin.stuuuff 3";

        private readonly string groupName = "group 1";
        private readonly string groupName2 = "group 2";
        private readonly string groupName3 = "group 3";

        private List<GroupPermission> permissions;
        private GroupPermission GroupPermissionCheckFalse;
        private GroupPermission GroupPermissionCheckTrue;

        [SetUp]
        public void SetUp()
        {
            GroupPermissionCheckTrue = new GroupPermission
            {
                GranteeGroup = new Group(groupName3, true),
                Privilege = new Privilege(privilegeName2),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
            };

            permissions = new List<GroupPermission>
            {
                new GroupPermission{
                GranteeGroup = new Group(groupName,true),
                Privilege = new Privilege(privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                },
                new GroupPermission{
                GranteeGroup = new Group(groupName2,true),
                Privilege = new Privilege(privilegeName3),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                }
            };
            PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(permissions.AsQueryable());
        }
        [Test]
        public void ShouldReturnTrue()
        {
            var result = GroupPermissionCheckTrue.CheckUnique(permissions);

            result.Should().BeTrue();
        }
    }
}

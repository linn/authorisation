namespace Linn.Authorisation.Domain.Tests.CheckingUniqueAndNotUniquePermissions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Tests.PermissionServiceTests;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCheckingGroupNotUnique : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";

        private readonly string privilegeName3 = "do.admin.stuuuff 3";

        private readonly string groupName = "group 1";

        private readonly string groupName2 = "group 2";

        private List<GroupPermission> permissions;

        private GroupPermission groupPermissionCheckFalse;

        [SetUp]
        public void SetUp()
        {
            this.groupPermissionCheckFalse = new GroupPermission
                                                 {
                                                     GranteeGroup = new Group(this.groupName, true),
                                                     Privilege = new Privilege(this.privilegeName),
                                                     GrantedByUri = "/employees/7004",
                                                     DateGranted = DateTime.UtcNow
                                                 };

            this.permissions = new List<GroupPermission>
                              {
                                  new GroupPermission
                                      {
                                          GranteeGroup = new Group(this.groupName, true),
                                          Privilege = new Privilege(this.privilegeName),
                                          GrantedByUri = "/employees/7004",
                                          DateGranted = DateTime.UtcNow
                                      },
                                  new GroupPermission
                                      {
                                          GranteeGroup = new Group(this.groupName2, true),
                                          Privilege = new Privilege(this.privilegeName3),
                                          GrantedByUri = "/employees/7004",
                                          DateGranted = DateTime.UtcNow
                                      }
                              };
            PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(this.permissions.AsQueryable());
        }

        [Test]
        public void ShouldReturnFalse()
        {
            var result = this.groupPermissionCheckFalse.CheckUnique(this.permissions);

            result.Should().BeFalse();
        }
    }
}

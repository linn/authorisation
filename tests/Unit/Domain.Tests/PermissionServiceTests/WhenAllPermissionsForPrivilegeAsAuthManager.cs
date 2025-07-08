namespace Linn.Authorisation.Domain.Tests.PermissionServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenAllPermissionsForPrivilegeAsAuthManager : ContextBase
    {
        private Group group;
        private Privilege privilege;
        private IEnumerable<Permission> result;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege
                                 {
                                     Name = "do.hings",
                                     Id = 1,
                                     Active = true
                                 };

            this.group = new Group
            {
                Name = "adminz",
                Active = true,
                Members = new List<Member> { new IndividualMember("/employees/133", "/employees/20") }
            };

            var userPrivileges = new List<string>
                                     {
                                         AuthorisedAction.AuthorisationAuthManager
                                     };

            var permissions = new List<Permission>
                              {
                                  new IndividualPermission("/employees/133", this.privilege, "/employees/7004"),
                                  new IndividualPermission("/employees/3006", this.privilege, "/employees/7004"),
                                  new GroupPermission(this.group, this.privilege, "/employees/7004"),
                              };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(permissions.AsQueryable());
            this.GroupRepository.FindAll().Returns(new List<Group> { this.group }.AsQueryable());
            this.result = this.Sut.GetAllPermissionsForPrivilege(1, userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(3);
        }

        [Test]
        public void ShouldCallPermissionsRepository()
        {
            this.PermissionRepository.Received().FilterBy(Arg.Any<Expression<Func<Permission, bool>>>());
        }
    }
}

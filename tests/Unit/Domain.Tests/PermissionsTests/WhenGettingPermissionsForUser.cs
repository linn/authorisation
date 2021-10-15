namespace Linn.Authorisation.Domain.Tests.PermissionsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Exceptions;
    using FluentAssertions;
    using Groups;
    using Linn.Authorisation.Domain;
    using NSubstitute;
    using NUnit.Framework;
    using Permissions;

    public class WhenGettingPermissionsForAUser : ContextBase
    {

        private IEnumerable<Permission> result;
        private readonly string privilegeName = "do.admin.stuuuff";

        [SetUp]
        public void SetUp()
        {
            var permissions = new List<Permission>
                                  {
                                      new IndividualPermission("/employees/133", new Privilege(privilegeName), "/employees/7004"),
                                      new IndividualPermission("/employees/3006", new Privilege(privilegeName), "/employees/7004"),
                                      new GroupPermission(new Group("adminz", true), new Privilege(privilegeName), "/employees/7004")
                                  };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(permissions.AsQueryable());

            this.GroupRepository.FindAll().Returns(new List<Group>().AsQueryable());

            this.result = this.Sut.GetAllPermissionsForUser("/employees/133");
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {

            var permissions = this.Sut.GetAllPermissionsForUser("/employees/133");
            var groupPermissions = new List<GroupPermission>();
            var individualPermissions = new List<IndividualPermission>();
            foreach (var p in permissions)
            {
                if (p is IndividualPermission)
                {
                    individualPermissions.Add((IndividualPermission)p);
                }
                else
                {
                    groupPermissions.Add((GroupPermission)p);
                }
            }

            permissions.ToList().Count.Should().Be(3);
            groupPermissions.Should().Contain(x => x.Privilege.Name == "do.admin.stuuuff" && x.GranteeGroup.Name == "adminz");
            individualPermissions.Should().Contain(x => x.Privilege.Name == "do.admin.stuuuff" && x.GranteeUri == "/employees/133");
            individualPermissions.Should().Contain(x => x.Privilege.Name == "do.admin.stuuuff" && x.GranteeUri == "/employees/3006");
        }
        [Test]
        public void ShouldCallGroupRepository()
        {
            this.GroupRepository.Received().FindAll();
        }

        [Test]
        public void ShouldCallPermissionsRepository()
        {
            this.PermissionRepository.Received().FilterBy(Arg.Any<Expression<Func<Permission, bool>>>());
        }

        [Test]
        public void ShouldNotReturnInactivePrivilege()
        {
            this.result.FirstOrDefault(x => x.Privilege.Name == "delete-things.admin" && x.Id == 3).Should().BeNull();
        }
    }
}

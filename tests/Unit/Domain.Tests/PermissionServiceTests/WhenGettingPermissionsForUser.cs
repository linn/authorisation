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

    public class WhenGettingPermissionsForUser : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";
        private readonly string privilegeName2 = "do-someother-stuuuff";
        private readonly string privilegeName3 = "do-hings";
        private IEnumerable<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var permissions = new List<Permission>
                                  {
                                      new IndividualPermission("/employees/133", new Privilege(this.privilegeName), "/employees/7004"),
                                      new IndividualPermission("/employees/3006", new Privilege(this.privilegeName2), "/employees/7004"),
                                      new GroupPermission(new Group("adminz", true), new Privilege(this.privilegeName3), "/employees/7004")
                                  };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(permissions.AsQueryable());
            this.GroupRepository.FindAll().Returns(new List<Group>().AsQueryable());
            this.result = this.Sut.GetAllPermissionsForUser("/employees/133");
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(3);
            this.result.Where(x => x is GroupPermission).Should().Contain(
                x => x.Privilege.Name == this.privilegeName3 && ((GroupPermission)x).GranteeGroup.Name == "adminz");
            this.result.Where(x => x is IndividualPermission).Should().Contain(
                x => x.Privilege.Name == this.privilegeName && ((IndividualPermission)x).GranteeUri == "/employees/133");
            this.result.Where(x => x is IndividualPermission).Should().Contain(
                x => x.Privilege.Name == this.privilegeName2 && ((IndividualPermission)x).GranteeUri == "/employees/3006");
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
            this.result.SingleOrDefault(x => x.Privilege.Name == "delete-things.admin" && x.Id == 3).Should().BeNull();
        }
    }
}

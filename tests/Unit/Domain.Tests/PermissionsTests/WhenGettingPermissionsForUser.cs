namespace Linn.Authorisation.Domain.Tests.PermissionsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using Groups;
    using Linn.Authorisation.Domain;
    using NSubstitute;
    using NUnit.Framework;
    using Permissions;

    public class WhenGettingPermissionsForAUser : ContextBase
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
            var groupPermissions = new List<GroupPermission>();
            var individualPermissions = new List<IndividualPermission>();
            foreach (var p in this.result)
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

            result.ToList().Count.Should().Be(3);
            groupPermissions.Should().Contain(x => x.Privilege.Name == this.privilegeName3 && x.GranteeGroup.Name == "adminz");
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

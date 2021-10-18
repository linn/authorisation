namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
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

    public class WhenGettingPermissionsForUserNoGroups : ContextBase
    {
        private IEnumerable<Privilege> result;

        [SetUp]
        public void SetUp()
        {
            var priv1 = new Privilege { Name = "click-buttons.admin", Id = 1, Active = true };
            var priv2 = new Privilege { Name = "type-things.admin", Id = 2, Active = true };
            var inactivePriv = new Privilege { Name = "delete-things.admin", Id = 3, Active = false };
            var indvPermissions = new List<Permission>
                                      {
                                          new IndividualPermission("/employees/33087", priv1, "/employees/7004"),
                                          new IndividualPermission("/employees/33087", priv2, "/employees/7004"),
                                          new IndividualPermission("/employees/33087", inactivePriv, "/employees/7004")
                                      };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(indvPermissions.AsQueryable());
            this.GroupRepository.FindAll().Returns(new List<Group>().AsQueryable());
            this.result = this.Sut.GetPrivileges("/employees/33087");
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
            this.result.FirstOrDefault(x => x.Name == "delete-things.admin" && x.Id == 3).Should().BeNull();
        }

        [Test]
        public void ShouldReturnActivePrivileges()
        {
            this.result.Count().Should().Be(2);
            this.result.FirstOrDefault(x => x.Name == "click-buttons.admin" && x.Id == 1).Should().NotBeNull();
            this.result.FirstOrDefault(x => x.Name == "type-things.admin" && x.Id == 2).Should().NotBeNull();
        }
    }
}

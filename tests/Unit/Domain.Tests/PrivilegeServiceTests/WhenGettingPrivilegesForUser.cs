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

    public class WhenGettingPrivilegesForUser : ContextBase
    {
        private IEnumerable<Privilege> result;

        [SetUp]
        public void SetUp()
        {
            var privilege1 = new Privilege { Name = "click-buttons.admin", Id = 1, Active = true };

            var privilege2 = new Privilege { Name = "type-things.admin", Id = 2, Active = true };

            var inactivePrivilege = new Privilege { Name = "delete-things.admin", Id = 3, Active = false };

            var indvPermissions = new List<Permission>
                                      {
                                          new IndividualPermission("/employees/33087", privilege1, "/employees/7004"),
                                          new IndividualPermission("/employees/33087", privilege2, "/employees/7004"),
                                          new IndividualPermission("/employees/33087", inactivePrivilege, "/employees/7004")
                                      };

            var group1 = new Group { Name = "be-cool", Active = true, };
            group1.AddIndividualMember("/employees/33087", "/employees/12345");
            group1.AddIndividualMember("not-the-right-member", "/employees/12345");

            var group2 = new Group { Name = "memberless-group", Active = true };

            var groups = new List<Group> { group1, group2 };

            this.GroupRepository.FindAll().Returns(groups.AsQueryable());

            var privOnlyInGroup = new Privilege { Name = "be-cool.admin", Id = 4, Active = true };

            var groupPermissionsToReturn = new List<Permission>
                                           {
                                               new GroupPermission(group1, privOnlyInGroup, "/employees/7004")
                                           };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(indvPermissions.AsQueryable(), groupPermissionsToReturn.AsQueryable());

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
            this.result.Count().Should().Be(3);
            this.result.FirstOrDefault(x => x.Name == "click-buttons.admin" && x.Id == 1).Should().NotBeNull();
            this.result.FirstOrDefault(x => x.Name == "type-things.admin" && x.Id == 2).Should().NotBeNull();
            this.result.FirstOrDefault(x => x.Name == "be-cool.admin" && x.Id == 4).Should().NotBeNull();
        }
    }
}
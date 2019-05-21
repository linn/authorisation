namespace Linn.Authorisation.Facade.Tests.PermissionTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain;
    using Domain.Groups;
    using Domain.Permissions;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingPrivilegesInGroupHierarchy : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            var subGroup = new Group("Group 1", true);
            subGroup.AddIndividualMember("/employees/1", "/employees/7004");

            var group = new Group("Group 2", true);
            group.AddGroupMember(subGroup, "/employees/7004");

            this.GroupRepository.GetGroups().Returns(
                new List<Group> { subGroup, group }.AsQueryable());

            this.PermissionRepository.GetIndividualPermissions("/employees/1").Returns(new List<Permission>());

            this.PermissionRepository.GetGroupsPermissions(Arg.Any<IEnumerable<Group>>())
                .Returns(new List<Permission> 
                    {
                        new GroupPermission(group,new Privilege("tariffs.created"),DateTime.UtcNow, "/employees/7004"),
                        new GroupPermission(subGroup,new Privilege("sernos.created"),DateTime.UtcNow, "/employees/7004"),
                    });

            this.result = this.Sut.GetPrivileges("/employees/1");
        }

        [Test]
        public void ShouldReturnPrivileges()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data;
            var enumerable = privileges.ToList();
            enumerable.Count.Should().Be(2);
            enumerable.First().Name.Should().Be("tariffs.created");
            enumerable.SingleOrDefault(p => p.Name == "tariffs.created").Should().NotBeNull();
        }
    }
}
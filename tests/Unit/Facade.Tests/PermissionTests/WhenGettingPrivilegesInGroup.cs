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

    public class WhenGettingPrivilegesInGroup : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);
            group.AddIndividualMember("/employees/1", "/employees/7004");
            this.GroupRepository.FindAll().Returns(
                new List<Group> { group }.AsQueryable());

            this.PermissionRepository.GetGroupsPermissions(Arg.Any<IEnumerable<Group>>())
                .Returns(new List<Permission>
                    {
                        new GroupPermission(group, new Privilege("tariffs.created"), DateTime.UtcNow, "/employees/7004"),
                    });

            this.result = this.Sut.GetPrivileges("/employees/1");
        }

        [Test]
        public void ShouldReturnPrivileges()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data;
            var enumerable = privileges.ToList();
            enumerable.Count.Should().Be(1);
            enumerable.First().Name.Should().Be("tariffs.created");
            enumerable.SingleOrDefault(p => p.Name == "tariffs.created").Should().NotBeNull();
        }
    }
}
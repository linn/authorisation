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

    public class WhenGettingPrivilegesInclAGroup : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("Test", true);
            group.AddIndividualMember("/employees/1", "/employees/7004");
   
            var individualPermissions = new List<Permission>
            {
                new IndividualPermission("/employees/1", new Privilege("sernos.created"), "/employees/7004"),
                new IndividualPermission("/employees/1", new Privilege("vatcodes.created"), "/employees/7004")
            };

            var groups = new List<Group> { group };
            this.GroupRepository.GetGroups().Returns(groups.AsQueryable());
            var groupPermissions = new List<Permission>
            {
                new GroupPermission(groups.FirstOrDefault(), new Privilege("tariffs.created"),DateTime.UtcNow, "/employees/7004")
            };

            this.PermissionRepository.GetIndividualPermissions("/employees/1").Returns(individualPermissions);
            this.PermissionRepository.GetGroupsPermissions(Arg.Any<IEnumerable<Group>>()).Returns(groupPermissions);

            this.result = this.Sut.GetPrivilegesForMember("/employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data;
            var enumerable = privileges.ToList();
            enumerable.Count.Should().Be(3);
            enumerable.SingleOrDefault(p => p.Name == "tariffs.created").Should().NotBeNull();
        }
    }
}
namespace Linn.Authorisation.Facade.Tests.PermissionTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Common.Facade;
    using Domain;
    using Domain.Groups;
    using Domain.Permissions;
    using FluentAssertions;

    using Linn.Authorisation.Facade.Tests.AuthorisationServiceTests;

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
            this.GroupRepository.FindAll().Returns(groups.AsQueryable());
            var groupPermissions = new List<Permission>
            {
                new GroupPermission(groups.First(), new Privilege("tariffs.created"), "/employees/7004")
            };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>()).Returns(individualPermissions.AsQueryable(), groupPermissions.AsQueryable());


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
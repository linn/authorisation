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

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(new List<Permission>
                    {
                        new GroupPermission(group, new Privilege("tariffs.created"), "/employees/7004"),
                    }.AsQueryable());

            this.result = this.Sut.GetPrivilegesForMember("/employees/1");
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
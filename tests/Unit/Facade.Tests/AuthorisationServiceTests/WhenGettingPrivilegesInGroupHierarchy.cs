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

    public class WhenGettingPrivilegesInGroupHierarchy : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            var subGroup = new Group("Group 1", true) { Id = 1 };
            subGroup.AddIndividualMember("/employees/1", "/employees/7004");

            var group = new Group("Group 2", true) { Id = 2 };
            group.AddGroupMember(subGroup, "/employees/7004");

            this.GroupRepository.FindAll().Returns(
                new List<Group> { subGroup, group }.AsQueryable());

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>()).Returns(new List<Permission>().AsQueryable());

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(new List<Permission> 
                    {
                        new GroupPermission(group,new Privilege("tariffs.created"), "/employees/7004"),
                        new GroupPermission(subGroup,new Privilege("sernos.created"), "/employees/7004"),
                    }.AsQueryable());

            this.result = this.Sut.GetPrivilegesForMember("/employees/1");
        }

        [Test]
        public void ShouldReturnPrivileges()                                               
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data;
            var enumerable = privileges.ToList();
            enumerable.Count.Should().Be(2);
            enumerable.First().Name.Should().Be("sernos.created");
            enumerable.SingleOrDefault(p => p.Name == "sernos.created").Should().NotBeNull() ;
        }
    }
}
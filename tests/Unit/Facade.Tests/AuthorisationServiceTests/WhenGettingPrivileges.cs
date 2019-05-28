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

    public class WhenGettingPrivileges : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            var individualPermissions = new List<Permission>
                              {
                                  new IndividualPermission("/employees/1", new Privilege("sernos.created"), "/employees/7004"),
                                  new IndividualPermission("/employees/1", new Privilege("vatcodes.created"), "/employees/7004"),
                                  new IndividualPermission("/employees/1", new Privilege("tariffs.created"), "/employees/7004"),
                              };

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>()).Returns(individualPermissions.AsQueryable(), new List<Permission>().AsQueryable());

            this.result = this.Sut.GetPrivilegesForMember("/employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data;
            privileges.Count().Should().Be(3);
        }
    }
}

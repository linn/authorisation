namespace Linn.Authorisation.Facade.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingUserPermissions : ContextBase
    {
        private IResult<IEnumerable<Permission>> result;

        private IEnumerable<Role> roles;

        [SetUp]
        public void SetUp()
        {
            this.roles = new List<Role>
                              {
                                  new Role
                                      {
                                        Permissions = new List<Permission>
                                        {
                                            new Permission("create.sernos"),
                                            new Permission("update.tariff")
                                        },
                                        Members = new List<string>()                             
                                      },
                                  new Role
                                      {
                                          Permissions = new List<Permission> { new Permission("update.vatcode") },
                                          Members = new List<string>()
                                      }
                              };

            this.RoleRepository.FilterBy(Arg.Any<Expression<Func<Role, bool>>>()).Returns(this.roles.AsQueryable());
            this.result = this.Sut.GetPermissions("employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Permission>>>();

            var Permissions = ((SuccessResult<IEnumerable<Permission>>)this.result).Data;
            Permissions.Count().Should().Be(3);
        }
    }
}

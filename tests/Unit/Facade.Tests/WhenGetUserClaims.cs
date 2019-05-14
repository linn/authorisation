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

    public class WhenGetUserClaims : ContextBase
    {
        private IResult<IEnumerable<Claim>> result;


        private IEnumerable<Role> roles;


        [SetUp]
        public void SetUp()
        {
            this.roles = new List<Role>
                              {
                                  new Role
                                      {
                                        Claims = new List<Claim>
                                        {
                                            new Claim("create.sernos"),
                                            new Claim("update.tariff")
                                        }
                                      },
                                  new Role
                                      {
                                          Claims = new List<Claim> { new Claim("update.vatcode") }
                                      }

                              };

            this.RoleRepository.FilterBy(Arg.Any<Expression<Func<Role, bool>>>()).Returns(this.roles.AsQueryable());
            this.result = this.Sut.GetClaims("employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Claim>>>();

            var claims = ((SuccessResult<IEnumerable<Claim>>)this.result).Data;
            claims.Count().Should().Be(3);
        }

    }
}

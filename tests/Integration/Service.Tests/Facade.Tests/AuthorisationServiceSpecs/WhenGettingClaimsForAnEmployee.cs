namespace Linn.Authorisation.Integration.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;

    using NUnit.Framework;

    public class WhenGettingClaimsForAnEmployee : ContextBase
    {
        private IResult<IEnumerable<Claim>> result;

        private IEnumerable<Role> roles;

        [SetUp]
        public void SetUp()
        {
            TestDbContext.Roles = new List<Role>
            {
                new Role
                {
                    Claims = new List<Claim>
                    {
                        new Claim("create.sernos"),
                        new Claim("update.tariff")
                    },
                    Members = new List<string>()
                },
                new Role
                {
                    Claims = new List<Claim> { new Claim("update.vatcode") },
                    Members = new List<string>()
                }
            };

            this.result = this.Sut.GetClaims("employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Claim>>>();

            var claims = ((SuccessResult<IEnumerable<Claim>>)this.result).Data;
            claims.Count().Should().Be(0);
        }
    }
}

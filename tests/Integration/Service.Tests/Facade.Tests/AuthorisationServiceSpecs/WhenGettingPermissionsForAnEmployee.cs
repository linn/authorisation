namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{ 
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Common.Facade;

    using NUnit.Framework;

    public class WhenGettingPermissionsForAnEmployee : ContextBase
    {
        private IResult<IEnumerable<Permission>> result;

        private IEnumerable<Role> roles;

        [SetUp]
        public void SetUp()
        {
            TestDbContext.Roles = new List<Role>
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

            this.result = this.Sut.GetPermissions("employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Permission>>>();

            var Permissions = ((SuccessResult<IEnumerable<Permission>>)this.result).Data;
            Permissions.Count().Should().Be(0);
        }
    }
}

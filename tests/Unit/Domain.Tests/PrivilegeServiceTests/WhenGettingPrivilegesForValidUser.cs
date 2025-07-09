namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForValidUser : ContextBase
    {
        private IEnumerable<Privilege> result;

        [SetUp]
        public void SetUp()
        {
            var privileges = new List<Privilege>
                                  {
                                      new Privilege("finance.auth-manager"),
                                      new Privilege("finance.do.stuuuff"),
                                      new Privilege("finance.do.hings"),
                                  };

            var userPrivileges = new List<string>
            {
                "finance.auth-manager",
            };

            this.PrivilegeRepository.FindAll()
                .Returns(privileges.AsQueryable());
            this.result = this.Sut.GetAllPrivilegesForUser(userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(3);
            this.result.Should().Contain(
                x => x.Name == "finance.auth-manager");
            this.result.Should().Contain(
                x => x.Name == "finance.do.stuuuff");
            this.result.Should().Contain(
                x => x.Name == "finance.do.hings");
        }

        [Test]
        public void ShouldNotReturnInactivePrivilege()
        {
            this.result.SingleOrDefault(x => x.Name == "delete-things.admin" && x.Id == 3).Should().BeNull();
        }
    }
}
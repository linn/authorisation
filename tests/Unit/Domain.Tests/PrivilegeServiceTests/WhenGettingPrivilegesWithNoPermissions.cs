namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesWithNoPermissions : ContextBase
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
                                     new Privilege("purchasing.do.hings"),
                                 };

            var userPrivileges = new List<string>();

            this.PrivilegeRepository.FindAll().Returns(privileges.AsQueryable());

            this.result = this.Sut.GetAllPrivilegesForUser(userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectGroups()
        {
            this.result.ToList().Count.Should().Be(0);
        }
    }
}

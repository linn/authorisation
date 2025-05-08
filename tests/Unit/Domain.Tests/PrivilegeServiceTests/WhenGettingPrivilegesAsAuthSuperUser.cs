namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesAsAuthSuperUser : ContextBase
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

            var userPrivileges = new List<string>
            {
                AuthorisedAction.AuthorisationAuthManager,
            };

            this.PrivilegeRepository.FindAll()
                .Returns(privileges.AsQueryable());

            this.result = this.Sut.GetAllPrivilegesForUser(userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(4);
        }
    }
}
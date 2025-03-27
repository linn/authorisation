namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForAuthSuperUser : ContextBase
    {
        private IEnumerable<Privilege> result;

        [SetUp]
        public void SetUp()
        {
            var privileges = new List<Privilege>
                                  {
                                      new Privilege("finance.super-user"),
                                      new Privilege("finance.do.stuuuff"),
                                      new Privilege("finance.do.hings"),
                                      new Privilege("purchasing.do.hings")
                                  };

            var userPrivileges = new List<String>
            {
                "authorisation.super-user"
            };

            this.PrivilegeRepository.FindAll()
                .Returns(privileges.AsQueryable());

            this.result = this.Sut.GetPrivilegesForPermission(userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(4);
            this.result.Should().Contain(
                x => x.Name == "finance.super-user");
            this.result.Should().Contain(
                x => x.Name == "finance.do.stuuuff");
            this.result.Should().Contain(
                x => x.Name == "finance.do.hings");
        }

        [Test]
        public void ShouldCallPermissionsRepository()
        {
            this.PrivilegeRepository.Received().FindAll();
        }

    }
}


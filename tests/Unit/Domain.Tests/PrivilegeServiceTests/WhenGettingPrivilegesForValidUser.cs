﻿namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;

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
                                      new Privilege("purchasing.do.hings"),
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
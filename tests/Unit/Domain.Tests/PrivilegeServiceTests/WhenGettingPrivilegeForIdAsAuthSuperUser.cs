namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegeForIdAsAuthSuperUser : ContextBase
    {
        private Privilege result;

        [SetUp]
        public void SetUp()
        {
            var wantedPrivilege = new Privilege { Name = "finance.do.stuuuff", Id = 2 };

            var userPrivileges = new List<String>
            {
                "authorisation.super-user"
            };

            this.PrivilegeRepository.FindById(2)
                .Returns(wantedPrivilege);

            this.result = this.Sut.GetPrivilegeById(2, userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.Should().NotBeNull();
        }

        [Test]
        public void ShouldCallPermissionsRepository()
        {
            this.PrivilegeRepository.Received().FindById(2);
        }

    }
}




namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class GetPrivilegeForUserById : ContextBase
    {
        private Privilege result;

        private List<Privilege> privileges;

        [SetUp]
        public void SetUp()
        {
            var wantedPrivilege = new Privilege { Name = "finance.do.stuuuff", Id = 2 };

            var privileges = new List<Privilege>
                {
                    new Privilege{Name = "finance.super-user", Id = 1},
                    wantedPrivilege,
                    new Privilege{Name = "finance.do.hings", Id = 3},
                    new Privilege{Name = "purchasing.do.hings", Id = 4}
                };

            var userPrivileges = new List<String>
                {
                    "finance.super-user"
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



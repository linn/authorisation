namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegeByIdAsAuthSuperUser : ContextBase
    {
        private Privilege result;

        [SetUp]
        public void SetUp()
        {
            var wantedPrivilege = new Privilege { Name = "finance.do.stuuuff", Id = 2 };

            var userPrivileges = new List<string>
            {
                AuthorisedAction.AuthorisationAuthManager,
            };

            this.PrivilegeRepository.FindById(2)
                .Returns(wantedPrivilege);

            this.result = this.Sut.GetPrivilegeById(2, userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermission()
        {
            this.result.Name.Equals("finance.do.stuuuff");
        }
    }
}
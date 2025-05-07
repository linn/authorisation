namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegeForUserById : ContextBase
    {
        private Privilege result;

        [SetUp]
        public void SetUp()
        {
            var wantedPrivilege = new Privilege { Name = "finance.do.stuuuff", Id = 2 };

            var userPrivileges = new List<string>
                {
                    "finance.auth-manager",
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
namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Exceptions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegeAsAnNonValidUser : ContextBase
    {
        private Exception exception;

        [SetUp]
        public void SetUp()
        {
            var wantedPrivilege = new Privilege { Name = "finance.do.stuuuff", Id = 2 };

            var userPrivileges = new List<string>
                                     {
                                         "manufacturing.stuff"
                                     };

            this.PrivilegeRepository.FindById(2)
                .Returns(wantedPrivilege);

            this.exception = Assert.Throws<UnauthorisedActionException>(
                () => this.Sut.GetPrivilegeById(2, userPrivileges));
        }

        [Test]
        public void ShouldThrowUnauthorisedActionException()
        {
            this.exception.Should().BeOfType<UnauthorisedActionException>();
        }
    }
}

namespace Linn.Authorisation.Domain.Tests.GroupServiceTests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByIdWithNoPrivileges : ContextBase
    {
        private Exception exception;

        [SetUp]
        public void SetUp()
        {
            var group = new Group { Id = 2, Name = "finance.cashbook.users" };

            var groups = new List<Group>
                             {
                                 new Group { Id = 1, Name = "finance.auth-manager" },
                                 group,
                                 new Group { Id = 3, Name = "finance.users" },
                                 new Group { Id = 4, Name = "purchasing.users" },
                             };

            var userPrivileges = new List<string>();

            this.GroupRepository.FindById(group.Id)
                .Returns(group);

            this.exception = Assert.Throws<UnauthorisedActionException>(
                () => this.Sut.GetGroupById(group.Id, userPrivileges));
        }

        [Test]
        public void ShouldReturnNull()
        {
            this.exception.Should().BeOfType<UnauthorisedActionException>();
        }
    }
}

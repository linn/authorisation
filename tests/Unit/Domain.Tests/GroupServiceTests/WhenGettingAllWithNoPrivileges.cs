﻿namespace Linn.Authorisation.Domain.Tests.GroupServiceTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Authorisation.Domain.Groups;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingAllWithNoPrivileges : ContextBase
    {
        private IEnumerable<Group> result;

        [SetUp]
        public void SetUp()
        {
            var groups = new List<Group>
                             {
                                 new Group { Id = 1, Name = "finance.auth-managers" },
                                 new Group { Id = 2, Name = "finance.cashbook.users" },
                                 new Group { Id = 3, Name = "finance.users" },
                                 new Group { Id = 4, Name = "purchasing.buyers" },
                             };

            var userPrivileges = new List<string>();

            this.GroupRepository.FindAll()
                .Returns(groups.AsQueryable());

            this.result = this.Sut.GetAllGroupsForUser(userPrivileges);
        }

        [Test]
        public void ShouldReturnEmptyList()
        {
            this.result.ToList().Count.Should().Be(0);
        }
    }
}

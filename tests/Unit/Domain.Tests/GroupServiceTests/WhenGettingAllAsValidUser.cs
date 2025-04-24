namespace Linn.Authorisation.Domain.Tests.GroupServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Authorisation.Domain.Groups;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingAllAsValidUser : ContextBase
    {
        private IEnumerable<Group> result;

        [SetUp]
        public void SetUp()
        {
            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "finance.super-user" },
                new Group { Id = 2, Name = "finance.do.stuuuff" },
                new Group { Id = 3, Name = "finance.do.hings" },
                new Group { Id = 4, Name = "purchasing.do.hings" },
            };

            var userPrivileges = new List<string>
            {
                "finance.thing.super-user",
            };

            this.GroupRepository.FindAll()
                .Returns(groups.AsQueryable());

            this.result = this.Sut.GetAllGroupsForUser(userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(3);
            this.result.Should().Contain(
                x => x.Name == "finance.super-user");
            this.result.Should().Contain(
                x => x.Name == "finance.do.stuuuff");
            this.result.Should().Contain(
                x => x.Name == "finance.do.hings");
        }
    }
}
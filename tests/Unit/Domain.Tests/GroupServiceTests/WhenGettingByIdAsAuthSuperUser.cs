namespace Linn.Authorisation.Domain.Tests.GroupServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Authorisation.Domain.Groups;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByIdAsAuthSuperUser : ContextBase
    {
        private Group result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group { Id = 1, Name = "finance.auth-managers" };

            var groups = new List<Group>
            {
                group,
                new Group { Id = 2, Name = "finance.users" },
                new Group { Id = 3, Name = "finance.cashbook.users" },
                new Group { Id = 4, Name = "purchasing.users" },
            };

            var userPrivileges = new List<string>
            {
                "authorisation.auth-manager",
            };

            this.GroupRepository.FindById(group.Id)
                .Returns(group);

            this.result = this.Sut.GetGroupById(group.Id, userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.Name.Equals("finance.auth-managers");
        }
    }
}
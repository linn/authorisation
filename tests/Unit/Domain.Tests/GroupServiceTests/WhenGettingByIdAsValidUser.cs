namespace Linn.Authorisation.Domain.Tests.GroupServiceTests
{
    using System.Collections.Generic;
    using Linn.Authorisation.Domain.Groups;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingByIdAsValidUser : ContextBase
    {
        private Group result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group { Id = 2, Name = "finance.cashbook.users" };

            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "finance.auth-managers" },
                group,
                new Group { Id = 3, Name = "finance.users" },
                new Group { Id = 4, Name = "purchasing.users" },
            };

            var userPrivileges = new List<string>
            {
                "finance.auth-manager",
            };

            this.GroupRepository.FindById(group.Id)
                .Returns(group);

            this.result = this.Sut.GetGroupById(group.Id, userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.Name.Equals("finance.cashbook.users");
        }
    }
}

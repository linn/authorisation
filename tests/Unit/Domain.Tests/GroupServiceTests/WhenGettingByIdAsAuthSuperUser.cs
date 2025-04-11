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
            var group = new Group { Id = 1, Name = "finance.super-user" };

            var groups = new List<Group>
            {
                group,
                new Group{Id = 2, Name ="finance.do.stuuuff"},
                new Group{ Id = 3, Name = "finance.do.hings" },
                new Group{ Id = 4, Name = "purchasing.do.hings" }
            };

            var userPrivileges = new List<String>
            {
                "authorisation.super-user"
            };

            this.GroupRepository.FindById(group.Id)
                .Returns(group);

            this.result = this.Sut.GetGroupById(group.Id, userPrivileges);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.Name.Equals("finance.super-user");
        }
    }
}
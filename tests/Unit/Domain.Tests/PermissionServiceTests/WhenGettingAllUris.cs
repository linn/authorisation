namespace Linn.Authorisation.Domain.Tests.PermissionServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllUris : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";
        private Group group;
        private GroupMember groupMember;
        private IndividualMember individualMember;
        private Privilege privilege;
        private List<string> result;

        [SetUp]
        public void SetUp()
        { 
            this.privilege = new Privilege { Id = 1, Name = this.privilegeName };

            this.groupMember = new GroupMember
                {
                    Id = 2,
                    Group = new Group
                    {
                        Name = "group2",
                        Id = 10,
                        Members = new List<Member>
                        {
                            new IndividualMember("/employees/4", "/employees/7004"),
                            new IndividualMember("/employees/1", "/employees/7004"),
                        },
                    },
                };

            this.individualMember = new IndividualMember("/employees/3", "/employees/7004");

            this.group = new Group
            {
                Id = 1,
                Name = "adminz",
                Active = true,
                Members = new List<Member> { this.groupMember, this.individualMember },
            };

            var permissions = new List<Permission>
                                  {
                                      new IndividualPermission("/employees/1", this.privilege, "/employees/7004"),
                                      new IndividualPermission("/employees/2", this.privilege, "/employees/7004"),
                                      new GroupPermission(this.group, this.privilege, "/employees/7004"),
                                  };
            this.result = this.Sut.GetAllGranteeUris(permissions);
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            this.result.ToList().Count.Should().Be(4);
        }

        [Test]
        public void ShouldCallPermissionsRepository()
        {
            this.result.Should().Contain("/employees/1");
            this.result.Should().Contain("/employees/2");
            this.result.Should().Contain("/employees/3");
            this.result.Should().Contain("/employees/4");
        }
    }
}
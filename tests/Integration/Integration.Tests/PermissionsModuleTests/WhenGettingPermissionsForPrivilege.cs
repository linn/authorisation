namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPermissionsForPrivilege : ContextBase
    {
        private Group group;
        private Privilege privilege;
        private List<Permission> permissions;
        private List<string> resource;
        private GroupMember groupMember;
        private IndividualMember individualMember;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege { Id = 1, Name = "Privilege 1" };

            this.groupMember = new GroupMember
            {
                Id = 2,
                Group = new Group { Name = "group2", Id = 10, Members = new List<Member> { new IndividualMember("/employees/4", "/employees/7004") } },
            };

            this.individualMember = new IndividualMember("/employees/3", "/employees/7004");

            this.group = new Group
            {
                Id = 1,
                Name = "adminz",
                Active = true,
                Members = new List<Member> { this.groupMember, this.individualMember },
            };

            this.permissions = new List<Permission>
            {
                new IndividualPermission("/employees/1", this.privilege, "/employees/7004"),
                new IndividualPermission("/employees/2", this.privilege, "/employees/7004"),
                new GroupPermission(this.group, this.privilege, "/employees/7004"),
            };

            this.resource = new List<string> { "/employees/1", "/employees/2", "/employees/3", "/employees/4" };

            this.DomainService.GetAllPermissionsForPrivilege(this.privilege.Id).Returns(
                this.permissions);

            this.DomainService.GetAllGranteeUris(this.permissions).Returns(this.resource);

            this.Response = this.Client.Get(
                "/authorisation/permissions/privilege?privilegeId=1",
                with => { with.Accept("application/json"); }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnJsonBody()
        {
            var resources = this.Response.DeserializeBody<List<string>>()?.ToArray();
            resources.Should().HaveCount(4);

            resources.Should().Contain("/employees/1");
            resources.Should().Contain("/employees/2");
            resources.Should().Contain("/employees/3");
            resources.Should().Contain("/employees/4");
        }
    }
}
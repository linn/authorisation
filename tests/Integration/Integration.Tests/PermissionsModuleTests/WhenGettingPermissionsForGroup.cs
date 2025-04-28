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

    public class WhenGettingPermissionsForGroup : ContextBase
    {
        private Group group;

        private List<Permission> permissions;

        [SetUp]
        public void SetUp()
        {
            this.group = new Group
            {
                Id = 2,
                Name = "testing-get-group"
            };

            this.permissions = new List<Permission>
            {
                new GroupPermission
                {
                    GranteeGroup = this.group,
                    Privilege = new Privilege { Id = 1, Name = "Privilege 1" }
                },
                new GroupPermission
                {
                    GranteeGroup = this.group,
                    Privilege = new Privilege { Id = 2, Name = "Privilege 2" }
                }
            };

            this.DomainService.GetImmediatePermissionsForGroup(2).Returns(this.permissions.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions/2",
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
            var resources = this.Response.DeserializeBody<IEnumerable<PermissionResource>>()?.ToArray();
            resources.Should().HaveCount(2);

            resources.Should().Contain(a => a.Privilege == "Privilege 1");
            resources.Should().Contain(a => a.Privilege == "Privilege 2");
        }
    }
}

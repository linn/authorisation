namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.group = new Group
            {
                Name = "test.group.1",
                Id = 1,
                Active = true
            };

            this.GroupService.GetGroupById(this.group.Id, Arg.Any<IEnumerable<string>>()).Returns(
                this.group);

            this.Response = this.Client.Get(
                $"/authorisation/groups/{this.group.Id}",
                with =>
                {
                    with.Accept("application/json");
                }).Result;
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
            var resource = this.Response.DeserializeBody<PrivilegeResource>();
            resource.Id.Should().Be(1);
            resource.Name.Should().Be("test.group.1");
            resource.Active.Should().BeTrue();
        }
    }
}

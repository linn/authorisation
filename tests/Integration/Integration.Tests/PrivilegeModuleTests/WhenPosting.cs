namespace Linn.Authorisation.Integration.Tests.PrivilegeModuleTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPosting : ContextBase
    {
        private PrivilegeResource resource;

        [SetUp]
        public void SetUp()
        {
            this.AuthService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.resource = new PrivilegeResource { Name = "test-permission" };

            this.Response = this.Client.PostAsJsonAsync("/authorisation/privileges", this.resource).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallAddRepository()
        { 
            this.PrivilegeRepository.Received().Add(Arg.Is<Privilege>(p => p.Name == "test-permission" && p.Active == true));
        }

        [Test]
        public void ShouldCommit()
        {
            this.TransactionManager.Received(1).Commit();
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
            var result = this.Response.DeserializeBody<PrivilegeResource>();
            result.Name.Should().Be("test-permission");
        }

        [Test]
        public void ShouldReturnJsonBodyWithRelLink()
        {
            var result = this.Response.DeserializeBody<PrivilegeResource>();
            result.Name.Should().Be("test-permission");
            result.Links.Should().Contain(link => link.Rel == "view");
        }

    }
}
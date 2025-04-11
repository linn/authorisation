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

    public class WhenPutting : ContextBase
    {
        private PrivilegeResource updatedResource;

        private Privilege current;

        [SetUp]
        public void SetUp()
        {
            this.AuthService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.updatedResource = new PrivilegeResource { Name = "new.name", Active = false, Id = 12};
            this.current = new Privilege { Id = 12, Name = "old.name", Active = true };

            this.PrivilegeRepository.FindById(this.current.Id).Returns(this.current);
            this.DomainService.GetPrivilegeById(12, Arg.Any<IEnumerable<string>>()).Returns(
                this.current
            );

            this.Response = this.Client.PutAsJsonAsync("/authorisation/privileges/12", this.updatedResource).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
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
        public void ShouldUpdate()
        {
            this.current.Name.Should().Be(this.updatedResource.Name);
            this.current.Active.Should().Be(this.updatedResource.Active);
        }

        [Test]
        public void ShouldReturnUpdated()
        {
            var result = this.Response.DeserializeBody<PrivilegeResource>();
            result.Name.Should().Be(this.updatedResource.Name);
        }
    }
}

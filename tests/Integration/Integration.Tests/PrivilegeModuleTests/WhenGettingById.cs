namespace Linn.Authorisation.Integration.Tests.PrivilegeModuleTests
{
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingById : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.PrivilegeRepository.FindById(1).Returns(new Privilege { Id = 1, Name = "name", Active =true});

            this.Response = this.Client.Get(
                "/authorisation/privileges/1",
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
            var resources = this.Response.DeserializeBody<PrivilegeResource>();
            resources.Id.Should().Be(1);
            resources.Name.Should().Be("name");
            resources.Active.Should().BeTrue();
        }
    }
}

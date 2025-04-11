namespace Linn.Authorisation.Integration.Tests.PrivilegeModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.AuthService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.DomainService.GetAllPrivilegesForUser(Arg.Any<IEnumerable<string>>()).Returns(
                new List<Privilege>
                    {
                        new Privilege { Name = "1" },
                        new Privilege { Name = "2" }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/privileges",
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
            var resources = this.Response.DeserializeBody<IEnumerable<PrivilegeResource>>()?.ToArray();
            resources.Should().HaveCount(2);
            resources.Should().Contain(a => a.Name == "1");
            resources.Should().Contain(a => a.Name == "2");
        }
    }
}

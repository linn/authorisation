namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.GroupRepository.FindAll().Returns(
                new List<Group>
                    {
                        new Group { Name = "1" },
                        new Group { Name = "2" }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/groups",
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
            var resources = this.Response.DeserializeBody<IEnumerable<GroupResource>>()?.ToArray();
            resources.Should().HaveCount(2);

            resources.Should().Contain(a => a.Name == "1");
            resources.Should().Contain(a => a.Name == "2");
        }
    }
}

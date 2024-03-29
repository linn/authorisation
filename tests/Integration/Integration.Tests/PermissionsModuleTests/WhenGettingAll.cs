namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.DomainService.GetAllPermissionsForUser("1234").Returns(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "1" }
                            },
                        new GroupPermission
                            {
                                Privilege = new Privilege { Name = "2" }
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions?who=1234",
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
            var resources = this.Response.DeserializeBody<IEnumerable<PermissionResource>>()?.ToArray();
            resources.Should().HaveCount(2);

            resources.Should().Contain(a => a.Privilege == "1");
            resources.Should().Contain(a => a.Privilege == "2");
        }
    }
}

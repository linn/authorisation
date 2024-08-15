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

    public class WhenGettingAllPermissionsForEmployee : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var group = new Group
                             {
                                 Id = 2,
                                 Name = "testing-get-group"
                             };

            this.DomainService.GetAllPermissionsForUser("/employees/1234").Returns(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "1" },
                                GranteeUri = "/employees/1234"
                            },
                        new GroupPermission
                            {
                                GranteeGroup = group,
                                Privilege = new Privilege { Name = "2" },
                                Id = 2
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions?who=/employees/1234",
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;

    using NSubstitute;

    using NUnit.Framework;
    using System.Net;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;

    internal class WhenGettingEmployeesWithAPermission: ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.DomainService.GetAllPermissionsForPrivilege(1).Returns(
                new List<Permission>
                        {
                            new IndividualPermission
                                {
                                    Privilege = new Privilege { Name = "test-individual-name", Id = 3 },
                                    GranteeUri = "/employees/1"
                                },
                            new GroupPermission
                                {
                                    Privilege = new Privilege { Name = "3" },
                                    GranteeGroup = new Group("test-group-name", true, 30)
                                }
                        }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions?privilegeId=3",
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
            resources.Should().HaveCount(0);

            resources.Should().Contain(a => a.Privilege == "test-individual-name");
            resources.Should().Contain(a => a.Privilege == "test-group-name");
        }
    }
}

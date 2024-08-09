using System.Collections.Generic;
using System.Linq;

namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;
    using System.Net;

    internal class WhenGettingPermissionsForPrivilege : ContextBase
    {
        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.group = new Group
                             {
                                 Id = 2,
                                 Name = "testing-get-group"
                             };
            this.group.AddIndividualMember("/employees/2", "/1");

            this.DomainService.GetAllPermissionsForPrivilege(privilegeId : 1).Returns(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "1", Id = 1 }, GranteeUri = "/employees/1"
                            },
                        new GroupPermission
                            { 
                                Privilege = new Privilege { Name = "2", Id = 1 },
                                GranteeGroup = this.group 
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions/privilege?privilegeId=1",
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

            resources.Should().Contain(a => a.GranteeUri == "/employees/1");
            resources.Should().Contain(a => a.GranteeUri == "/employees/2");
        }
    }
}


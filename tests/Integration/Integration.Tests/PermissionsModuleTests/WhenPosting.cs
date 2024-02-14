namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingIndividualPermission : ContextBase
    {
        private PermissionResource resource;

        [SetUp]
        public void SetUp()
        {
            this.PrivilegeRepository.FindById(100).Returns(new Privilege { Id = 100, Name = "test.privilege", Active = true });

            this.resource = new PermissionResource
                                {
                                    Privilege = "test.privilege",
                                    PrivilegeId = 100,
                                    GrantedByUri = "/employees/33156",
                                    GranteeUri = "/employees/33190",
                                    DateGranted = DateTime.Now.ToString(),
                                    GranteeGroupId = null,
                                    GroupName = null
                                };

            this.Response = this.Client.PostAsJsonAsync("/authorisation/permissions", this.resource).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallAddRepository()
        { 
            this.PermissionRepository.Received().Add(Arg.Is<Permission>(p => p.Privilege.Id == 100 && p.GrantedByUri == "/employees/33156"));
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
            var result = this.Response.DeserializeBody<PermissionResource>();
            result.PrivilegeId.Should().Be(this.resource.PrivilegeId);
            result.GranteeUri.Should().Be(this.resource.GranteeUri);
        }
    }
}

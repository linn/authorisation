namespace Linn.Authorisation.Service.Tests.PermissionsModuleSpecs
{
    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Permission.Service.Tests.PermissionsModuleSpecs;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingPermission : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource { GranteeUri = "/employee/1", GrantedByUri = "/employee/33087", Privilege = "Name"};

            var p = new IndividualPermission("/employee/1", new Privilege("Name"), "/employee/33087");

            this.PermissionService.CreatePermission(Arg.Any<PermissionResource>())
                .Returns(new CreatedResult<Permission>(p));

            this.Response = this.Browser.Post(
                "/authorisation/permissions",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.JsonBody(resource);
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PermissionService.CreatePermission(Arg.Any<PermissionResource>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PermissionResource>();
            resource.GranteeUri.Should().Be("/employee/1");
        }
    }
}

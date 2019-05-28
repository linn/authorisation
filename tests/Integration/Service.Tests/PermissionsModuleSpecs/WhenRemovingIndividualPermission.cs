namespace Linn.Authorisation.Service.Tests.PermissionsModuleSpecs
{
    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRemovingIndividualPermission : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource { GranteeUri = "/employee/1", GrantedByUri = "/employee/33087", Privilege = "Name" };

            var p = new IndividualPermission("/employee/1", new Privilege("Name"), "/employee/33087");

            this.PermissionService.RemovePermission(Arg.Any<PermissionResource>())
                .Returns(new SuccessResult<Permission>(p));

            this.Response = this.Browser.Delete(
                "/authorisation/permissions",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                        with.Query("granteeUri", "/employee/1");
                        with.Query("privilege", "create");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PermissionService.RemovePermission(Arg.Any<PermissionResource>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PermissionResource>();
            resource.GranteeUri.Should().Be("/employee/1");
        }
    }
}
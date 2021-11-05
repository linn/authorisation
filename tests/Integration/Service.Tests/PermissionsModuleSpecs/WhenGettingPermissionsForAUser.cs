namespace Linn.Authorisation.Service.Tests.PermissionsModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingPermissionsForAUser : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";

        [SetUp]
        public void SetUp()
        {
            var resource = new IndividalPermissionRequestResource { GranteeUri = "/employees/33087" };

            var p = new GroupPermission(new Group("group", true), new Privilege("Name"), "/employees/33087");
            var permissions = new List<Permission>
                                            { 
                                                new IndividualPermission("/employees/33087", new Privilege(privilegeName), "/employees/7004"),
                                                new IndividualPermission("/employees/33087", new Privilege(privilegeName), "/employees/7004"),
                                                new GroupPermission(new Group("adminz", true), new Privilege(privilegeName), "/employees/7004")
                                            };
            
            this.PermissionService.GetAllPermissionsForUser(Arg.Any<string>())
                .Returns(new SuccessResult<IEnumerable<Permission>>(permissions));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Get(
                "/authorisation/permissions/user", 
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PermissionService.GetAllPermissionsForUser(Arg.Any<string>()).Received(1);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PermissionResource>>();
            resource.Count().Should().Be(3);
            resource.Count(x => !string.IsNullOrEmpty(x.GroupName)).Should().Be(1);
            resource.Count(x => !string.IsNullOrEmpty(x.GranteeUri)).Should().Be(2);
        }
    }
}

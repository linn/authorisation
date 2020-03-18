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

    public class WhenGettingAllPermissionsForPrivilege : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";

        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource { GroupName = "group", GrantedByUri = "/employee/33087", Privilege = "Name" };

            var p = new GroupPermission(new Group("group", true), new Privilege("Name"), "/employee/33087");
            var permissions = new List<Permission>
                                            {
                                                new IndividualPermission("/employees/133", new Privilege(privilegeName), "/employees/7004"),
                                                new IndividualPermission("/employees/3006", new Privilege(privilegeName), "/employees/7004"),
                                                new GroupPermission(new Group("adminz", true), new Privilege(privilegeName), "/employees/7004")
                                            };


            this.PermissionService.GetAllPermissionsForPrivilege(Arg.Any<int>())
                .Returns(new SuccessResult<IEnumerable<Permission>>(permissions));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Get(
                "/authorisation/permissions/3",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
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
            this.PermissionService.GetAllPermissionsForPrivilege(Arg.Any<int>()).Received(1);
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

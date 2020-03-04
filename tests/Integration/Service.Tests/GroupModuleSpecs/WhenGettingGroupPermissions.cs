namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    using Linn.Production.Domain.LinnApps;

    public class WhenGettingGroupPermissions : ContextBase
    {
        private readonly string firstGroup = "first group";
        
        [SetUp]
        public void SetUp()
        {
            var group1 = new Group(this.firstGroup, true);

            this.PermissionService.GetImmediatePermissionsForGroup(33)
                .Returns(
                    new SuccessResult<IEnumerable<Permission>>(
                        new List<GroupPermission>
                            {
                                new GroupPermission(group1, new Privilege("test priv"), "me")
                            }));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Get(
                "/authorisation/groups/33/permissions",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOK()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PermissionService.Received().GetImmediatePermissionsForGroup(33);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PermissionResource>>();
            resource.Should().HaveCount(1);
            resource.Any(x => x.GroupName == this.firstGroup).Should().BeTrue();
        }
    }
}

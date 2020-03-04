namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenRemovingIndividualPrivilege : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var requestResource = new PrivilegeResource { Name = "New privilege", Active = false };

            var p = new Privilege("Name");

            this.PrivilegeService.Remove(Arg.Any<int>())
                .Returns(new SuccessResult<string>("Successfully removed privilege 19 and associated permissions"));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Delete(
                "/authorisation/privileges/19",
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
            this.PrivilegeService.Remove(Arg.Any<int>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<string>();
            resource.Should().Be("Successfully removed privilege 19 and associated permissions");
        }
    }
}

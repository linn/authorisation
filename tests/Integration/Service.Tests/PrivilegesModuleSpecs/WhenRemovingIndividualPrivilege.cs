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
            var p = new Privilege("deletme");
            this.PrivilegeService.Remove(Arg.Any<int>())
                .Returns(new SuccessResult<Privilege>(p));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Delete(
                "/authorisation/privileges/19",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            var resource = this.Response.Body.DeserializeJson<Privilege>();
            resource.Name.Should().Be("deletme");
        }
    }
}

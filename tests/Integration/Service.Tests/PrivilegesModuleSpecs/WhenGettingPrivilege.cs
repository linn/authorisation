namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using System.Collections.Generic;

    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;

    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenGettingPrivilege : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege("First Privilege", true);
            this.PrivilegeService.GetById(19)
                .Returns(new SuccessResult<Privilege>(privilege));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);


            this.Response = this.Browser.Get(
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
            this.PrivilegeService.Received().GetById(19);
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PrivilegeResource>();
            resource.Name.Should().Be("First Privilege");
            resource.Active.Should().BeTrue();
        }
    }
}

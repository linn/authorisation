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

    public class WhenUpdatingPrivilege : ContextBase
    {
        private PrivilegeResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new PrivilegeResource { Name = "New privilege", Active = false };
            var privilege = new Privilege("New privilege", false);

            this.PrivilegeService.Update(19, Arg.Any<PrivilegeResource>())
                .Returns(new SuccessResult<Privilege>(privilege));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Put(
                "/authorisation/privileges/19",
                 with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
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
            this.PrivilegeService.Received().Update(
                19,
                Arg.Is<PrivilegeResource>(r => r.Name == this.requestResource.Name));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PrivilegeResource>();
            resource.Name.Should().Be("New privilege");
            resource.Active.Should().BeFalse();
        }
    }
}

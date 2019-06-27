namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;
    using System.Collections.Generic;

    public class WhenGettingPrivileges : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege("Test", true);
            this.PrivilegeService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Privilege>>(new List<Privilege> { privilege }));
            this.Response = this.Browser.Get(
                "/authorisation/privileges/all",
                with =>
                    {
                        with.Header("Accept", "application/json");
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
            this.PrivilegeService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<PrivilegeResource>>();
            resource.Should().HaveCount(1);
        }
    }
}

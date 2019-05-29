namespace Linn.Authorisation.Service.Tests.AuthorisationModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForMember : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var result = new List<Privilege> { new Privilege("test"), new Privilege("test2") };
            this.AuthorisationService.GetPrivilegesForMember("/employees/1234")
                .Returns(new SuccessResult<IEnumerable<Privilege>>(result));
            this.Response = this.Browser.Get(
                "/authorisation/privileges",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Query("who", "/employees/1234");
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
            this.AuthorisationService.GetPrivilegesForMember(Arg.Any<string>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resources = this.Response.Body
                .DeserializeJson<IEnumerable<PrivilegeResource>>().ToList();
            resources.Should().HaveCount(2);
        }
    }
}

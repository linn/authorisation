namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using FluentAssertions;

    using Linn.Authorisation.Resources;

    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenCreatingPrivilege : ContextBase
    {

        [SetUp]
        public void SetUp()
        {
            var resource = new PrivilegeResource { Name = "Test" };
            this.Response = this.Browser.Post(
                "/privileges",
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
            this.PrivilegeService.Add(Arg.Any<PrivilegeResource>()).ReceivedCalls();
        }
    }
}

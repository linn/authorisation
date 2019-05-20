namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using FluentAssertions;
    using Nancy;
    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivileges : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Get(
                "/privileges/1234",
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
            this.AuthorisationService.GetPrivileges("1234").ReceivedCalls();
        }
    }
}

namespace Linn.Authorisation.Service.Tests.AuthorisationModuleSpecs
{
    using FluentAssertions;
    using Nancy;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForMember : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.Response = this.Browser.Get(
                "/privileges/employees/1234",
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
            this.AuthorisationService.GetPrivilegesForMember("/employees/1234").ReceivedCalls();
        }
    }
}

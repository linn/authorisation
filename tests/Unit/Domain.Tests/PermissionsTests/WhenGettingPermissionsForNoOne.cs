namespace Linn.Authorisation.Domain.Tests.PermissionsTests
{
    using Linn.Authorisation.Domain.Exceptions;

    using NUnit.Framework;

    public class WhenGettingPermissionsForNoOne : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void ShouldThrowException()
        {
            Assert.Throws<NoGranteeUriProvidedException>(() => this.Sut.GetAllPermissionsForUser(string.Empty));
        }
    }
}

namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenDeletingPermission : ContextBase
    {
        private IndividualPermission permission;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.permission = new IndividualPermission("/employees/1", new Privilege("test-privilege"), "/employees/2");

            this.PermissionRepository.FindById(1).Returns(this.permission);

            this.Response = this.Client.Delete(
                "/authorisation/permissions/1",
                with =>
                    {
                        with.Accept("application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOK()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldRemoveFromRepository()
        {
            this.PermissionRepository.Received().Remove(this.permission);
            this.TransactionManager.Received(1).Commit();
        }
    }
}

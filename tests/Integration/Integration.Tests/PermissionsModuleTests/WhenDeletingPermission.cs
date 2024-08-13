namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenDeletingPermission : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.FacadeService.DeletePermission(1).Returns(new SuccessResult<PermissionResource>(new PermissionResource
                {
                    Id = 1
                }));

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
        public void ShouldRemoveFromRepository()
        {
            var resources = this.Response.DeserializeBody<IEnumerable<PermissionResource>>()?.ToArray();
            resources.Should().HaveCount(0);

            this.FacadeService.Received().DeletePermission(1);
            this.TransactionManager.Received(1).Commit();
        }
    }
}

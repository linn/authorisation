namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
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
                        Id = 1,
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
            this.FacadeService.DeletePermission(1);
        }
    }
}

namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;

    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenTryingToDeleteInvalidPermission : ContextBase
    {
        private IndividualPermission permission;

        [SetUp]
        public void SetUp()
        {
            this.permission = new IndividualPermission("/employees/1", new Privilege("test-privilege"), "/employees/2");


            this.PermissionRepository.FindById(1).ReturnsNull();


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
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

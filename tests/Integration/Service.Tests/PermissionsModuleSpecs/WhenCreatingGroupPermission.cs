﻿namespace Linn.Authorisation.Service.Tests.PermissionsModuleSpecs
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenCreatingGroupPermission : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource { GroupName = "group", GrantedByUri = "/employee/33087", Privilege = "Name" };

            var p = new GroupPermission(new Group("group", true), new Privilege("Name"), "/employee/33087");

            this.PermissionService.CreatePermission(Arg.Any<PermissionResource>())
                .Returns(new CreatedResult<Permission>(p));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Post(
                "/authorisation/permissions",
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
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallService()
        {
            this.PermissionService.CreatePermission(Arg.Any<PermissionResource>()).ReceivedCalls();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<PermissionResource>();
            resource.GroupName.Should().Be("group");
        }
    }
}

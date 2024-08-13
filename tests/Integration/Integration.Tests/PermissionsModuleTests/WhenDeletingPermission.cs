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

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingPermission : ContextBase
    {
        private PermissionResource resource;

        private int id;

        private IndividualPermission permission;

        private List<Permission> permissions;

        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege { Id = 100, Name = "test.privilege", Active = true };
            
            this.permission = new IndividualPermission 
                                  { 
                                      Id = 1, Privilege = privilege, GranteeUri = "/employees/1"
                                  };

            this.permissions = new List<Permission> { this.permission };

           // this.FacadeService.DeletePermission(1).Returns(new IndividualPermission { Id = 1, Privilege = null });

            this.Response = this.Client.DeleteFromJsonAsync("/authorisation/permissions/1").Result;
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

namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingAndPermissionAlreadyExistsForThisPrivilegeAndGrantee : ContextBase
    {
        private PermissionResource resource;

        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege { Id = 100, Name = "test.privilege", Active = true };
            this.PrivilegeRepository.FindById(privilege.Id).Returns(
                privilege);

            var existingPermission = new IndividualPermission
                                         {
                                             Id = 10, Privilege = privilege, GranteeUri = "/employees/33190"
                                         };

            this.PermissionRepository.FindAll().Returns(new List<Permission>
                                                            {
                                                                existingPermission
                                                            }.AsQueryable());

            this.resource = new PermissionResource
            {
                Privilege = privilege.Name,
                PrivilegeId = privilege.Id,
                GrantedByUri = existingPermission.GrantedByUri,
                GranteeUri = existingPermission.GranteeUri,
            };

            this.Response = this.Client.PostAsJsonAsync("/authorisation/permissions", this.resource).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ShouldNotCommit()
        {
            this.TransactionManager.DidNotReceive().Commit();
        }
    }
}

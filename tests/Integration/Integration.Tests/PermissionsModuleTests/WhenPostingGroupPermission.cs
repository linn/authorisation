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

    public class WhenPostingGroupPermission : ContextBase
    {
        private PermissionResource resource;

        [SetUp]
        public void SetUp()
        {
            var privilege = new Privilege { Id = 100, Name = "test.privilege", Active = true };
            this.PrivilegeRepository.FindById(privilege.Id).Returns(privilege);

            var groupPermission = new GroupPermission
                                      {
                                          Id = 10,
                                          Privilege = privilege,
                                          GrantedByUri = "/employees/33156",
                                          DateGranted = DateTime.Now,
                                          GranteeGroup = new Group
                                                             { 
                                                                 Id = 10,
                                                                 Active = true,
                                                                 Name = "TestGroupName"
                                                             }
            };

            this.PermissionRepository.FindById(groupPermission.Id).Returns(groupPermission);

            this.resource = new PermissionResource
                                {
                                    Privilege = privilege.Name,
                                    PrivilegeId = privilege.Id,
                                    GrantedByUri = groupPermission.GrantedByUri,
                                    GranteeGroupId = groupPermission.GranteeGroup.Id,
                                    GroupName = groupPermission.GranteeGroup.Name,
                                    DateGranted = groupPermission.DateGranted.ToString("o"),
            };

            this.Response = this.Client.PostAsJsonAsync("/authorisation/permissions", this.resource).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCallAddRepository()
        { 
            this.PermissionRepository.Received().Add(Arg.Is<GroupPermission>(p => p.Privilege.Id == 100 && p.GranteeGroup.Id == 11));
        }

        [Test]
        public void ShouldCommit()
        {
            this.TransactionManager.Received(1).Commit();
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnJsonBody()
        {
            var result = this.Response.DeserializeBody<PermissionResource>();
            result.PrivilegeId.Should().Be(this.resource.PrivilegeId);
            result.GranteeGroupId.Should().Be(this.resource.GranteeGroupId);
        }
    }
}

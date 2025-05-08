namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllPermissionForAPrivilege : ContextBase
    {
        private Privilege privilege;

        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationAuthManager, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.group = new Group
                            {
                                Id = 2,
                                Name = "testing-get-group",
                                Members = new List<Member>
                                    {
                                        new IndividualMember
                                            {
                                                Id = 4,
                                                MemberUri = "/employees/1234"
                                            },
                                        new GroupMember
                                            {
                                                Id = 5,
                                                Group = new Group
                                                    {
                                                        Id = 6,
                                                        Name = "testing-get-group-2",
                                                        Members = new List<Member>
                                                            {
                                                                new IndividualMember
                                                                    {
                                                                        Id = 6,
                                                                        MemberUri = "/employees/5678"
                                                                    }
                                                            }
                                                    }
                                            }
                                    }
            };

            this.privilege = new Privilege { Id = 1, Name = "Test Privilege" };

            this.DomainService.GetAllPermissionsForPrivilege(this.privilege.Id, Arg.Any<IEnumerable<string>>()).Returns(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Id = 1,
                                Privilege = this.privilege,
                                GranteeUri = "/employees/1234"
                            },
                        new GroupPermission
                            {
                                GranteeGroup = this.group,
                                Privilege = this.privilege,
                                Id = 2
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions/privilege?privilegeId=1",
                with =>
                    {
                        with.Accept("application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
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
        public void ShouldReturnJsonBody()
        {
            var resources = this.Response.DeserializeBody<IEnumerable<PermissionResource>>()?.ToArray();
            resources.Should().HaveCount(2);

            resources.Should().Contain(a => a.Id == 1);
            resources.Should().Contain(a => a.Id == 2);
        }
    }
}

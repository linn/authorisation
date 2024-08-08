namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Collections.Generic;
    using System.Linq;

    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;

    using NSubstitute;

    using NUnit.Framework;
    using System.Net;

    public class WhenDeletingPermission : ContextBase
    {
        /*[SetUp]
        public void SetUp()
        {
            DomainService.DeletePermission(100)(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "1" },
                                Id = 100
                            },
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "2" },
                                Id = 101
                                
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions?who=100",
                with =>
                    {
                        with.Accept("application/json");
                    }).Result;
        }*/

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // [Test]
        // public void ShouldCallService()
        // {
        //     this.PermissionRepository.Received().DeletePermission(Arg.Any<int>(), 123);
        // }
    }
}

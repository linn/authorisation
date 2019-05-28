namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenCreatingPrivilegeForNonExistentPrivilege : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource
                               {
                                   GrantedByUri = "/employees/33087",
                                   Privilege = "created",
                                   GranteeUri = "bob",
                               };
            this.PrivilegeRepository.FilterBy(p => p.Name == resource.Privilege).ReturnsNull();
            this.result = this.Sut.CreatePermission(resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<Permission>>();
        }
    }
}
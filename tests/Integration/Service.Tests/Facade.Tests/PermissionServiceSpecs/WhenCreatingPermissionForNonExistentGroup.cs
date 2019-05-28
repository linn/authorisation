namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using System;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using NSubstitute;
    using NSubstitute.ReturnsExtensions;

    using NUnit.Framework;

    public class WhenCreatingPrivilegeForNonExistentGroup : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource
                               {
                                   GroupName = "asdfghjkl",
                                   GrantedByUri = "/employees/33087",
                                   Privilege = "created"
                               };

            this.GroupRepository.FilterBy(g => g.Name == resource.GroupName).ReturnsNull();
            this.result = this.Sut.CreatePermission(resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<Permission>>();
        }
    }
}
namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using FluentAssertions;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using NUnit.Framework;

    public class WhenRemovingAmbiguousPermission : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource
                                              {
                                                  GranteeUri = "/employees/1234",
                                                  GroupName = "group",
                                                  Privilege = "create",
                                                  GrantedByUri = "/employees/33087"
                                              };

            this.result = this.Sut.RemovePermission(resource);
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.result.Should().BeOfType<BadRequestResult<Permission>>();
        }
    }
}
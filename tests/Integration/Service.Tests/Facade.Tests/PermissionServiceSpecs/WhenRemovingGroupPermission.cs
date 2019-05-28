namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using FluentAssertions;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using NUnit.Framework;

    public class WhenRemovingGroupPermission : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource
                               {
                                   GroupName = "group",
                                   Privilege = "create",
                                   GrantedByUri = "/employees/33087"
                               };

            this.result = this.Sut.RemovePermission(resource);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            var permission = ((SuccessResult<Permission>)this.result).Data;
            permission.Should().BeOfType<GroupPermission>();
            this.result.Should().BeOfType<SuccessResult<Permission>>();
        }
    }
}
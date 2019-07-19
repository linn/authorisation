namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRemovingGroupPermission : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var group = new Group("group", true);
            var resource = new PermissionResource
                               {
                                   GroupName = "group",
                                   Privilege = "create",
                                   GrantedByUri = "/employees/33087"
                               };
            this.PrivilegeRepository.FilterBy(Arg.Any<Expression<Func<Privilege, bool>>>())
                .Returns(new List<Privilege> { new Privilege("create") }.AsQueryable());
            this.GroupRepository.FilterBy(Arg.Any<Expression<Func<Group, bool>>>())
                .Returns(new List<Group> { group }.AsQueryable());

            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(new List<Permission> { new GroupPermission(group, new Privilege(), "me") }.AsQueryable());
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
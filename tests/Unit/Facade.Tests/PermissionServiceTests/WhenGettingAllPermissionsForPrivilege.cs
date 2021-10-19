namespace Linn.Authorisation.Facade.Tests.PermissionServiceTests
{
    using System;
    using System.Collections;

    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Facade;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    public class WhenGettingAllPermissionsForPrivilege : ContextBase
    {
        private IResult<IEnumerable<Permission>> result;

        private readonly string privilegeName = "do.admin.stuuuff";
        [SetUp]
        public void SetUp()
        {

            var permissions = new List<Permission>
                                            {
                                                new IndividualPermission("/employees/133", new Privilege(privilegeName), "/employees/7004"),
                                                new IndividualPermission("/employees/3006", new Privilege(privilegeName), "/employees/7004"),
                                                new GroupPermission(new Group("adminz", true), new Privilege(privilegeName), "/employees/7004")
                                            };
            this.PermissionService.GetAllPermissionsForPrivilege(Arg.Any<int>()).Returns(permissions);
            this.result = this.Sut.GetAllPermissionsForPrivilege(1);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Permission>>>();
        }

        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            var permissions = ((SuccessResult<IEnumerable<Permission>>)this.result).Data;

            permissions.ToList().Count.Should().Be(3);
            permissions.Where(x => x is GroupPermission).Should().Contain(x => x.Privilege.Name == this.privilegeName && ((GroupPermission)x).GranteeGroup.Name == "adminz");
            permissions.Where(x => x is IndividualPermission).Should().Contain(x => x.Privilege.Name == this.privilegeName && ((IndividualPermission)x).GranteeUri == "/employees/133");
            permissions.Where(x => x is IndividualPermission).Should().Contain(x => x.Privilege.Name == this.privilegeName && ((IndividualPermission)x).GranteeUri == "/employees/3006");
        }
    }
}

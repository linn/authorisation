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

    public class WhenGettingPermissionsForAUser : ContextBase
    {
        private IResult<IEnumerable<Permission>> result;

        private readonly string privilegeName = "do.admin.stuuuff";
        private readonly string privilegeName2 = "do-someother-stuuuff";
        private readonly string privilegeName3 = "do-hings";

        [SetUp]
        public void SetUp()
        {

            var permissions = new List<Permission>
                                            {
                                                new IndividualPermission("/employees/133", new Privilege(this.privilegeName), "/employees/7004"),
                                                new IndividualPermission("/employees/3006", new Privilege(this.privilegeName2), "/employees/7004"),
                                                new GroupPermission(new Group("adminz", true), new Privilege(this.privilegeName3), "/employees/7004")
                                            };

            this.PermissionService.GetAllPermissionsForUser(Arg.Any<string>()).Returns(permissions);


            this.result = this.Sut.GetAllPermissionsForUser("/employees/133");
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
            var groupPermissions = new List<GroupPermission>();
            var individualPermissions = new List<IndividualPermission>();
            foreach (var p in permissions)
            {
                if (p is IndividualPermission)
                {
                    individualPermissions.Add((IndividualPermission)p);
                }
                else
                {
                    groupPermissions.Add((GroupPermission)p);
                }
            }

            permissions.ToList().Count.Should().Be(3);
            groupPermissions.Should().Contain(x => x.Privilege.Name == this.privilegeName3 && x.GranteeGroup.Name == "adminz");
            individualPermissions.Should().Contain(x => x.Privilege.Name == privilegeName && x.GranteeUri == "/employees/133");
            individualPermissions.Should().Contain(x => x.Privilege.Name == privilegeName2 && x.GranteeUri == "/employees/3006");
        }
    }
}

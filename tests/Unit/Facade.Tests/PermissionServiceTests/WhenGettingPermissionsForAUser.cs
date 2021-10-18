namespace Linn.Authorisation.Facade.Tests.PermissionServiceTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Facade;
    using NSubstitute;
    using NUnit.Framework;

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
            permissions.ToList().Count.Should().Be(3);
            permissions.Where(x => x is GroupPermission).Should().Contain(x => x.Privilege.Name == this.privilegeName3 && ((GroupPermission)x).GranteeGroup.Name == "adminz");
            permissions.Where(x => x is IndividualPermission).Should().Contain(x => x.Privilege.Name == this.privilegeName && ((IndividualPermission)x).GranteeUri == "/employees/133");
            permissions.Where(x => x is IndividualPermission).Should().Contain(x => x.Privilege.Name == this.privilegeName2 && ((IndividualPermission)x).GranteeUri == "/employees/3006");
        }
    }
}

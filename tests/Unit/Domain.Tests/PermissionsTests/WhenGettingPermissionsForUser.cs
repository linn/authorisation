namespace Linn.Authorisation.Domain.Tests.PermissionsTests
{
    using System;

    using Exceptions;
    using FluentAssertions;
    using Groups;
    using NUnit.Framework;
    using Permissions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Linn.Authorisation.Domain;


    using NSubstitute;

    public class WhenGettingPermissionsForAUser : ContextBase
    {

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

            this.Sut.GetAllPermissionsForUser(Arg.Any<string>()).Returns(permissions);


           var result = this.Sut.GetAllPermissionsForUser("/employees/133");
        }


        [Test]
        public void ShouldReturnCorrectPermissions()
        {
            var permissions = this.Sut.GetAllPermissionsForUser("/employees/133");
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
            groupPermissions.Should().Contain(x => x.Privilege.Name == privilegeName && x.GranteeGroup.Name == "adminz");
            individualPermissions.Should().Contain(x => x.Privilege.Name == privilegeName && x.GranteeUri == "/employees/133");
            individualPermissions.Should().Contain(x => x.Privilege.Name == privilegeName && x.GranteeUri == "/employees/3006");
        }
    }
}
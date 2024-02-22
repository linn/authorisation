namespace Linn.Authorisation.Domain.Tests.PermissionServiceTests
{
    using FluentAssertions;
    using Linn.Authorisation.Domain.Permissions;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class WhenCheckingUniqueAndNotUnique : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";
        private readonly string privilegeName2 = "do.admin.stuuuff 2";
        private readonly string privilegeName3 = "do.admin.stuuuff 3";

        private List<IndividualPermission> permissions;
        private IndividualPermission individualPermissionCheckFalse;
        private IndividualPermission individualPermissionCheckTrue;

        [SetUp]
        public void SetUp()
        {
            this.individualPermissionCheckFalse = new IndividualPermission
            {
                GranteeUri = "/employees/133",
                Privilege = new Privilege(this.privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
            };

            this.individualPermissionCheckTrue = new IndividualPermission
            {
                GranteeUri = "/employees/100",
                Privilege = new Privilege(this.privilegeName2),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
            };

            this.permissions = new List<IndividualPermission>
            {
                new IndividualPermission{
                GranteeUri = "/employees/133",
                Privilege = new Privilege(this.privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                },
                new IndividualPermission{
                GranteeUri = "/employees/3006",
                Privilege = new Privilege(this.privilegeName3),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                }
            };
            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(this.permissions.AsQueryable());
        }
            [Test]
            public void ShouldReturnFalse()
            {
                var result = this.individualPermissionCheckFalse.CheckUnique(this.permissions);

                result.Should().BeFalse();
            }

            public void ShouldReturnTrue()
            {
                var result = this.individualPermissionCheckTrue.CheckUnique(this.permissions);

                result.Should().BeTrue();
            }
    }
}

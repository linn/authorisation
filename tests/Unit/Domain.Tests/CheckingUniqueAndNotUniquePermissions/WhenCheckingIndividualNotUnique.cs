namespace Linn.Authorisation.Domain.Tests.CheckingUniqueAndNotUniquePermissions
{
    using FluentAssertions;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Tests.PermissionServiceTests;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class WhenCheckingIndividualNotUnique : ContextBase
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
            individualPermissionCheckFalse = new IndividualPermission
            {
                GranteeUri = "/employees/133",
                Privilege = new Privilege(privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
            };

            permissions = new List<IndividualPermission>
            {
                new IndividualPermission{
                GranteeUri = "/employees/133",
                Privilege = new Privilege(privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                },
                new IndividualPermission{
                GranteeUri = "/employees/3006",
                Privilege = new Privilege(privilegeName3),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                }
            };
            PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(permissions.AsQueryable());
        }
        [Test]
        public void ShouldReturnFalse()
        {
            var result = individualPermissionCheckFalse.CheckUnique(permissions);

            result.Should().BeFalse();
        }
    }
}

using Linn.Authorisation.Domain.Permissions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linn.Authorisation.Domain.Tests.PermissionServiceTests
{
    public class WhenCheckingUniqueAndNotUnique : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";
        private List<IndividualPermission> permissions;
        private IndividualPermission individualPermission;

        [SetUp]
        public void SetUp()
        {
            individualPermission = new IndividualPermission
            {
                GranteeUri = "/employees/133",
                Privilege = new Privilege(this.privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
            };

            permissions = new List<IndividualPermission>
            {
                new IndividualPermission{
                GranteeUri = "/employees/133",
                Privilege = new Privilege(this.privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                }
                ,
                new IndividualPermission{
                GranteeUri = "/employees/3006",
                Privilege = new Privilege(this.privilegeName),
                GrantedByUri = "/employees/7004",
                DateGranted = DateTime.UtcNow
                }
            };
            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                .Returns(permissions.AsQueryable());
        }
            [Test]
            public void ShouldReturnFalse()
            {
                var result = individualPermission.CheckUnique(permissions);

                Assert.IsFalse(result);
            }
    }
}

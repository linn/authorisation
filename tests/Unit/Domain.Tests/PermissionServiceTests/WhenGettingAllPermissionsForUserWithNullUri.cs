namespace Linn.Authorisation.Domain.Tests.PermissionServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAllPermissionsForUserWithNullUri : ContextBase
    {
        private readonly string privilegeName = "do.admin.stuuuff";
        private readonly string privilegeName2 = "do-someother-stuuuff";
        private readonly string privilegeName3 = "do-hings";
        private Exception result;

        [SetUp]
        public void SetUp()
        {
            var permissions = new List<Permission>
                                  {
                                      new IndividualPermission("/employees/133", new Privilege(this.privilegeName), "/employees/7004"),
                                      new IndividualPermission("/employees/3006", new Privilege(this.privilegeName2), "/employees/7004"),
                                      new GroupPermission(new Group("adminz", true), new Privilege(this.privilegeName3), "/employees/7004"),
                                  };

            this.result = Assert.Throws<NoGranteeUriProvidedException>(
                () => this.Sut.GetAllPermissionsForUser(null));
        }

        [Test]
        public void ShouldThrowUnauthorisedActionException()
        {
            this.result.Should().BeOfType<NoGranteeUriProvidedException>();
        }
    }
}


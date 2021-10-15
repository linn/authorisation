namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPermissionsForUser : ContextBase
    {
        private IEnumerable<Privilege> result;

        [SetUp]
        public void SetUp()
        {
            var priv1 = new Privilege { Name = "click-buttons.admin" };

            var priv2 = new Privilege { Name = "type-things.admin" };

            var indvPermissions = new List<IndividualPermission>
                                      {
                                          new IndividualPermission("/employees/33087", priv1, "/employees/7004"),
                                          new IndividualPermission("/employees/33087", priv2, "/employees/7004"),
                                      };

           


            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                    .Returns(indvPermissions);

            this.GroupRepository.FindAll()
                    .Returns(new List<Group>());


            this.result = this.Sut.GetPrivileges("/employees/33087");
        }

        [Test]
        public void ShouldReturnPrivilegs()
        {
            this.result.Count().Should().Be(2);
            this.result.FirstOrDefault(
                x => x.Name == "click-buttons.admin").Should().NotBeNull();
            this.result.FirstOrDefault(
                x => x.Name == "type-things.admin").Should().NotBeNull();
        }

        [Test]
        public void ShouldCallPermissionsRepository()
        {
            this.PermissionRepository.Received().FilterBy(Arg.Any<Expression<Func<Permission, bool>>>());
        }

        [Test]
        public void ShouldCallGroupRepository()
        {
            this.GroupRepository.Received().FindAll());
        }
    }
}

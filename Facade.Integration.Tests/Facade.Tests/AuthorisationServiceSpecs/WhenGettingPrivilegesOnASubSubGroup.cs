namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain;
    using FluentAssertions;

    using global::Facade.Integration.Tests.Facade.Tests;

    using NUnit.Framework;

    public class WhenGettingPrivilegesOnASubSubGroup : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            TestDbContext.BuildPrivilege("create.sernos");
            TestDbContext.BuildPrivilege("update.tariff");
            TestDbContext.BuildPrivilege("update.vatcode");

            var subSubGroup = TestDbContext.BuildGroup("sub-sub", true).BuildGroupMember("/employees/1");
            var subGroup = TestDbContext.BuildGroup("sub", true).BuildGroupMember(subSubGroup);
            var group = TestDbContext.BuildGroup("master", true).BuildGroupMember(subGroup);
            TestDbContext.BuildPermission(group, "create.sernos");
            TestDbContext.BuildPermission(subGroup, "update.tariff");

            this.result = this.Sut.GetPrivilegesForMember("/employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data.ToList();
            privileges.Count().Should().Be(2);
            privileges.First().Name.Should().Be("create.sernos");
            privileges.SingleOrDefault(p => p.Name == "update.tariff").Should().NotBeNull();
        }
    }
}
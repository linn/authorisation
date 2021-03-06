namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain;
    using FluentAssertions;

    using global::Facade.Integration.Tests.Facade.Tests;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForAnEmpWithinAGroup : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            TestDbContext.BuildPrivilege("create.sernos");
            TestDbContext.BuildPrivilege("update.tariff");
            TestDbContext.BuildPrivilege("update.vatcode");

            TestDbContext.BuildPermission("/employees/2", "update.tariff");
            TestDbContext.BuildPermission("/employees/2", "update.vatcode");

            var group = TestDbContext.BuildGroup("test", true).BuildGroupMember("/employees/1");
            TestDbContext.BuildPermission(group, "create.sernos");

            this.result = this.Sut.GetPrivilegesForMember("/employees/1");
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data.ToList();
            privileges.Count().Should().Be(1);
            privileges.First().Name.Should().Be("create.sernos");
        }
    }
}
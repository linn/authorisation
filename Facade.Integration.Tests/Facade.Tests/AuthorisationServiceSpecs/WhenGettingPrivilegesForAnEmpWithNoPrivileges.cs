namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain;
    using FluentAssertions;

    using global::Facade.Integration.Tests.Facade.Tests;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForAnEmpWithNoPrivileges : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            TestDbContext.BuildPrivilege("create.sernos");
            TestDbContext.BuildPrivilege("update.tariff");
            TestDbContext.BuildPrivilege("update.vatcode");

            TestDbContext.BuildPermission("/employees/1", "create.sernos");
            TestDbContext.BuildPermission("/employees/2", "update.tariff");
            TestDbContext.BuildPermission("/employees/2", "update.vatcode");

            this.result = this.Sut.GetPrivilegesForMember("/employees/3");
        }

        [Test]
        public void ShouldReturnSuccessButNoPrivileges()
        {
            this.result.Should().BeOfType<SuccessResult<IEnumerable<Privilege>>>();

            var privileges = ((SuccessResult<IEnumerable<Privilege>>)this.result).Data.ToList();
            privileges.Count().Should().Be(0);
        }
    }
}
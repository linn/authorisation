namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenGettingPrivilegesForAnEmpWithinAGroup : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
            TestDbContext.BuildPrivilege("create.sernos");
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
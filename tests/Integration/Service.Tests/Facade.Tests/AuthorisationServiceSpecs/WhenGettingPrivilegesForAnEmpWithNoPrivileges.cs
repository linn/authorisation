namespace Linn.Authorisation.Service.Tests.Facade.Tests.AuthorisationServiceSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    public class WhenGettingPrivilegesForAnEmpWithNoPrivileges : ContextBase
    {
        private IResult<IEnumerable<Privilege>> result;

        [SetUp]
        public void SetUp()
        {
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
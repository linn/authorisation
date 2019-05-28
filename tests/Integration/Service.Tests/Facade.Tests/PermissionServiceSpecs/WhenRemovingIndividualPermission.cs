namespace Linn.Authorisation.Service.Tests.Facade.Tests.PermissionServiceSpecs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenRemovingIndividualPermission : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var resource = new PermissionResource
                                              {
                                                  GranteeUri = "/employees/1234",
                                                  Privilege = "create",
                                                  GrantedByUri = "/employees/33087"
                                              };
            this.PrivilegeRepository.FilterBy(Arg.Any<Expression<Func<Privilege, bool>>>())
                .Returns(new List<Privilege> { new Privilege("create") }.AsQueryable());
            this.result = this.Sut.RemovePermission(resource);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            var permission = ((SuccessResult<Permission>)this.result).Data;
            permission.Should().BeOfType<IndividualPermission>();
            this.result.Should().BeOfType<SuccessResult<Permission>>();
        }
    }
}
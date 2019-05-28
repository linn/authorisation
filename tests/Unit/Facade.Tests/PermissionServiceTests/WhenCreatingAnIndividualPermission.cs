namespace Linn.Authorisation.Facade.Tests.PermissionServiceTests
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

    public class WhenCreatingAnIndividualPermission : ContextBase
    {
        private IResult<Permission> result;

        [SetUp]
        public void SetUp()
        {
            var permission = new PermissionResource
                                 {
                                     GrantedByUri = "/employees/1", GranteeUri = "/employees/3306", Privilege = "create"
                                 };

            var privilege = new Privilege("create");

            this.PrivilegeRepository.FilterBy(Arg.Any<Expression<Func<Privilege, bool>>>()).Returns(new List<Privilege> { privilege}.AsQueryable());

            this.result = this.Sut.Add(permission);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<CreatedResult<Permission>>();

            var permission = ((CreatedResult<Permission>)this.result).Data;
            permission.Privilege.Name.Should().Be("create");
            permission.GrantedByUri.Should().Be("/employees/1");
        }
    }
}

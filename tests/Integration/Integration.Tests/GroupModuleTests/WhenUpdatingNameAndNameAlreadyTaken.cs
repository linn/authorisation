using Linn.Authorisation.Domain;

namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;

    using NSubstitute;
    using NUnit.Framework;

    public class WhenUpdatingNameAndNameAlreadyTaken : ContextBase
    {
        private Group updatedResource;

        private List<Group> currentGroups;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.updatedResource = new Group { Name = "Group.Test.Name-2", Active = true, Id = 30};
            this.currentGroups = new List<Group>
                               {
                                    new Group( "Group.Test.Name-1", false, 30),
                                    new Group( "Group.Test.Name-2", false, 31 )
                               };
            
            this.GroupRepository.FindById(this.updatedResource.Id).Returns(this.updatedResource);

            this.GroupRepository.FilterBy(Arg.Any<Expression<Func<Group, bool>>>())
                .Returns(this.currentGroups.AsQueryable());
            this.Response = this.Client.PutAsJsonAsync("/authorisation/groups/30", this.updatedResource).Result;
        }

        [Test]
        public void ShouldReturnBadRequest()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ShouldNotCommit()
        {
            this.TransactionManager.DidNotReceive().Commit();
        }
    }
}

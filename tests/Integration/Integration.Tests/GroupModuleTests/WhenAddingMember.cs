using Linn.Authorisation.Domain;
using Linn.Common.Facade;
using Microsoft.AspNetCore.Http;

namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Json;
    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;
    using NUnit.Framework;

    public class WhenAddingMember : ContextBase
    {
        private MemberResource newResource;

        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.group = new Group { Id = 1, Name = "Group 1" };

            this.GroupService.GetGroupById(this.group.Id, Arg.Any<IEnumerable<string>>()).Returns(
                this.group);

            this.newResource = new MemberResource
            {
                GroupId  = 1,
                MemberUri = "employee-1"
            };

            this.MembersFacadeService
                .AddIndividualMember(new MemberResource(), "employee-1", Arg.Any<IEnumerable<string>>())
                .Returns((IResult<MemberResource>)this.newResource);

            this.Response = this.Client.PutAsJsonAsync("/authorisation/members", this.newResource).Result;
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnUpdated()
        {
            var result = this.Response.DeserializeBody<MemberResource>();
            result.MemberUri.Should().Be(this.newResource.MemberUri);
            result.GroupId.Should().Be(1);
        }
    }
}

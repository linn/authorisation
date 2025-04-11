using System.Runtime.InteropServices.JavaScript;
using Linn.Authorisation.Domain;

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

    public class WhenPosting : ContextBase
    {
        private MemberResource resource;

        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.group = new Group 
                            { 
                                Id = 20, 
                                Name = "test.group", 
                                Active = true
                            };

            this.Response = this.Client.PostAsJsonAsync("/authorisation/groups", this.group).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldCommit()
        {
            this.TransactionManager.Received(1).Commit();
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }
    }
}

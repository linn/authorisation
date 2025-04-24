namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
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
        private MemberResource resource;

        [SetUp]
        public void SetUp()
        {
            var group = new Group
            {
                Id = 20,
                Name = "test.group",
                Active = true
            };

            this.GroupRepository.FindById(group.Id).Returns(group);

            this.resource = new MemberResource { MemberUri = "test-member", GroupId = 20 };

            this.Response = this.Client.PostAsJsonAsync("/authorisation/members", this.resource).Result;
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

        [Test]
        public void ShouldReturnJsonBody()
        {
            var result = this.Response.DeserializeBody<MemberResource>();
            result.MemberUri.Should().Be("test-member");
        }
    }
}
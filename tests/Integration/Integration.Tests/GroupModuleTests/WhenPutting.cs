namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System.Net;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using Linn.Authorisation.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPutting : ContextBase
    {
        private GroupResource updatedResource;

        private Group current;

        [SetUp]
        public void SetUp()
        {
            this.updatedResource = new GroupResource { Name = "new.Group.Test.Name", Active = false, Id = 30} ;

            this.current = new Group { Id = 30, Name = "old.Group.Test.Name", Active = true };
            this.GroupRepository.FindById(this.current.Id).Returns(this.current);
            this.Response = this.Client.PutAsJsonAsync("/authorisation/groups/30", this.updatedResource).Result;
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
        public void ShouldUpdate()
        {
            this.current.Name.Should().Be(this.updatedResource.Name);
            this.current.Active.Should().Be(this.updatedResource.Active);
        }

        [Test]
        public void ShouldReturnUpdated()
        {
            var result = this.Response.DeserializeBody<GroupResource>();
            result.Name.Should().Be(this.updatedResource.Name);
        }
    }
}

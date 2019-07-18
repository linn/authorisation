namespace Linn.Authorisation.Service.Tests.GroupsModuleSpecs
{
    using FluentAssertions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Tests.GroupModuleSpecs;
    using Linn.Common.Facade;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    class WhenUpdatingGroup : ContextBase
    {
        private GroupResource requestResource;

        [SetUp]
        public void SetUp()
        {
            this.requestResource = new GroupResource { Name = "New group", Active = false };
            var group = new Group("New group", false);
          
            this.GroupService.Update(19, Arg.Any<GroupResource>())
                .Returns(new SuccessResult<Group>(group));
            
            this.Response = this.Browser.Put(  
                "/authorisation/groups/19",
                 with =>
                {
                    with.Header("Accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.JsonBody(this.requestResource);
                }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.GroupService.Received().Update(
                19,
                Arg.Is<GroupResource>(r => r.Name == this.requestResource.Name));
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<GroupResource>();
            resource.Name.Should().Be("New group");
            resource.Active.Should().BeFalse();
        }
    }
}

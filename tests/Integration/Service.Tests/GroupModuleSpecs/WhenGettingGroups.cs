namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Facade;
    using Domain.Groups;
    using FluentAssertions;
    using Linn.Production.Domain.LinnApps;
    using Nancy;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenGettingGroups : ContextBase
    {
        private readonly string firstGroup = "first group";

        private readonly string secondGroup = "second group";

        [SetUp]
        public void SetUp()
        {
            var group1 = new Group(this.firstGroup, true);
            var group2 = new Group(this.secondGroup, true);
            this.GroupService.GetAll()
                .Returns(new SuccessResult<IEnumerable<Group>>(new List<Group> { group1, group2 }));

            this.AuthorisationService.HasPermissionFor(
                AuthorisedAction.AuthorisationAdmin,
                Arg.Any<IEnumerable<string>>()).Returns(true);

            this.Response = this.Browser.Get(
                "/authorisation/groups",
                with =>
                    {
                        with.Header("Accept", "application/json");
                        with.Header("Content-Type", "application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOK()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldCallService()
        {
            this.GroupService.Received().GetAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            var resource = this.Response.Body.DeserializeJson<IEnumerable<GroupResource>>();
            resource.Should().HaveCount(2);
            resource.Any(x => x.Name == this.firstGroup).Should().BeTrue();
            resource.Any(x => x.Name == this.secondGroup).Should().BeTrue();
        }
    }
}

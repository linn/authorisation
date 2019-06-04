namespace Linn.Authorisation.Facade.Tests.GroupServiceTests
{
    using Common.Facade;
    using Domain;
    using Domain.Groups;
    using Domain.Permissions;
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;

    public class WhenCreatingAGroup : ContextBase
    {
        private IResult<Group> result;

        [SetUp]
        public void SetUp()
        {
            var group = new GroupResource
            {
                Name = "Test",
                Active = true
            };

            this.result = this.Sut.Add(group);
        }

        [Test]
        public void ShouldReturnSuccess()
        {
            this.result.Should().BeOfType<CreatedResult<Group>>();

            var group = ((CreatedResult<Group>)this.result).Data;
            group.Name.Should().Be("Test");
            group.Active.Should().BeTrue();
        }
    }
}
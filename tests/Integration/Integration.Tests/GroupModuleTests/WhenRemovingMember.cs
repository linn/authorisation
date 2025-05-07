namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System.Collections.Generic;
    using System.Net;
    using FluentAssertions;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Integration.Tests.Extensions;
    using NSubstitute;
    using NUnit.Framework;

    public class WhenRemovingMember : ContextBase
    {
        private IndividualMember member;

        [SetUp]
        public void SetUp()
        {
            this.AuthorisationService.HasPermissionFor(AuthorisedAction.AuthorisationSuperUser, Arg.Any<IEnumerable<string>>())
                .Returns(true);

            this.member = new IndividualMember { Id = 1, MemberUri = "/employees/1" };

            this.MemberRepository.FindById(member.Id).Returns(this.member);

            this.Response = this.Client.Delete(
                "/authorisation/members/1",
                with =>
                {
                    with.Accept("application/json");
                }).Result;
        }

        [Test]
        public void ShouldReturnOK()
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
        public void ShouldRemoveFromRepository()
        {
            this.MemberRepository.Received().Remove(this.member);
            this.TransactionManager.Received(1).Commit();
        }
    }
}


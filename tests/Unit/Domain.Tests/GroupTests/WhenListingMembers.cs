namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using System.Linq;

    using FluentAssertions;

    using Linn.Authorisation.Domain.Groups;

    using NUnit.Framework;

    public class WhenListingMembers
    {
        // This is a test to prove that Groups and members successfully implement the composite design pattern
        // https://www.geeksforgeeks.org/composite-design-pattern-in-java/
        // i.e. iterating over a Groups 'members' list will yield not only its
        // direct individual members, but also the individuals members
        // of any inner groups within the group, or indeed inner-inner groups within that first
        // inner group, and so on ad infinitum
        private Group Sut { get; set; }
        
        [SetUp]
        public void SetUp()
        {
            this.Sut = new Group { Name = "OUTER GROUP", Id = 1 };
            var innerGroup = new Group { Name = "INNER GROUP", Id = 2 };
            var innerInnerGroup = new Group { Name = "INNER INNER GROUP", Id = 3 };
            
            // the innermost group has one Individual Member
            innerInnerGroup.AddIndividualMember("oliver", string.Empty);
            
            // the 'middle' group has three Individual member
            innerGroup.AddIndividualMember("ross", string.Empty);
            innerGroup.AddIndividualMember("kyle", string.Empty);
            innerGroup.AddIndividualMember("david", string.Empty);

            // and also of course contains the innermost group
            innerGroup.AddGroupMember(innerInnerGroup, string.Empty);
            
            // The outermost Group i.e. the group under test
            // has two individual members
            this.Sut.AddIndividualMember("andrew", string.Empty);
            this.Sut.AddIndividualMember("colin", string.Empty);
            
            // and contains one group, the middle group
            this.Sut.AddGroupMember(innerGroup, string.Empty);
        }

        [Test]
        public void ShouldTraverseWholeStructure()
        {
            // test that accessing the outer groups members list
            // yields all 6 members of the nested structure
            this.Sut.MemberUris().Count().Should().Be(6);
        }
    }
}

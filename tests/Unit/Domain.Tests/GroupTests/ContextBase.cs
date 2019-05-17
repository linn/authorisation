namespace Linn.Authorisation.Domain.Tests.GroupTests
{
    using Groups;
    using NUnit.Framework;

    public class ContextBase
    {
        protected Group Sut { get; private set; }
       
        [SetUp]
        public void SetUpContext()
        {
            this.Sut = new Group("Test", true);
        }
    }
}

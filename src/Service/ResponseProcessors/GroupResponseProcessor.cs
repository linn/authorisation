namespace Linn.Authorisation.Service.ResponseProcessors
{
    using Common.Facade;
    using Common.Nancy.Facade;
    using Domain.Groups;

    public class GroupResponseProcessor : JsonResponseProcessor<Group>
    {
        public GroupResponseProcessor(IResourceBuilder<Group> resourceBuilder)
            : base(resourceBuilder, "privilege", 1)
        {
        }
    }
}
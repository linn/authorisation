namespace Linn.Authorisation.Resources
{
    using Common.Resources;

    public class GroupResource : HypermediaResource
    {
        public string Name { get; set; }

        public bool Active { get; set; }

    }
}
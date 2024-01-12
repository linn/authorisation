namespace Linn.Authorisation.Resources
{
    using Linn.Common.Resources;

    public class PrivilegeResource : HypermediaResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}

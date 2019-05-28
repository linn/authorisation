namespace Linn.Authorisation.Resources
{
    using Linn.Common.Resources;

    public class AuthorisationRequestResource : HypermediaResource
    {
        public string Who { get; set; }
    }
}
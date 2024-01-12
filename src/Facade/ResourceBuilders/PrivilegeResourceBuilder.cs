namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PrivilegeResourceBuilder : IBuilder<Privilege>
    {

        public object Build(Privilege model, IEnumerable<string> claims)
        {
            return new PrivilegeResource
                       {
                           Id = model.Id,
                           Name = model.Name,
                           Active = model.Active,
                           Links = this.BuildLinks(model).ToArray()
                       };
        }
        
        public string GetLocation(Privilege model)
        {
            return $"/authorisation/privileges/{model.Id}";
        }
        
        object IBuilder<Privilege>.Build(Privilege model, IEnumerable<string> claims) => this.Build(model, claims);
        
        private IEnumerable<LinkResource> BuildLinks(Privilege privilege)
        {
            yield return new LinkResource
                             {
                                 Rel = "self",
                                 Href = $"/privileges/{privilege.Id}"
                             };
        }
    }
}

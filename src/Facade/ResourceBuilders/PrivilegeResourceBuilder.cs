namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PrivilegeResourceBuilder : IResourceBuilder<Privilege>
    {
        public PrivilegeResource Build(Privilege privilege)
        {
            return new PrivilegeResource
                       {
                           Name = privilege.Name,
                           Active = privilege.Active,
                           Links = this.BuildLinks(privilege).ToArray()
                       };
        }
        
        object IResourceBuilder<Privilege>.Build(Privilege p) => this.Build(p);

        public string GetLocation(Privilege model)
        {
            return $"/authorisation/privileges/{model.Name}";
        }

        public IEnumerable<LinkResource> BuildLinks(Privilege privilege)
        {
            yield return new LinkResource
                             {
                                 Rel = "self",
                                 Href = $"/privileges/{privilege.Id}"
                             };
        }
    }
}
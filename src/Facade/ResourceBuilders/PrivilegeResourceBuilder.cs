namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PrivilegeResourceBuilder : IBuilder<Privilege>
    {
        private readonly IAuthorisationService authService;

        public PrivilegeResourceBuilder(
            IAuthorisationService authService)
        {
            this.authService = authService;
        }

        public object Build(Privilege model, IEnumerable<string> claims)
        {
            var privileges = claims == null ? new List<string>() : claims.ToList();
            var department = model.Name.Split('.')[0];

            if ((this.authService.HasPermissionFor($"{department}.admin", privileges) ||
                 this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges)) && model != null)
            {
                return new PrivilegeResource
                {
                    Id = model.Id,
                    Name = model.Name,
                    Active = model.Active,
                    Links = this.BuildLinks(model,claims).ToArray()
                };
            }

            return null;
        }
        
        public string GetLocation(Privilege model)
        {
            return $"/authorisation/privileges/{model.Id}";
        }
        
        object IBuilder<Privilege>.Build(Privilege model, IEnumerable<string> claims) => this.Build(model, claims);
        
        private IEnumerable<LinkResource> BuildLinks(Privilege model, IEnumerable<string> claims)
        {
            var privileges = claims == null ? new List<string>() : claims.ToList();

            if (model != null)
            {
                yield return new LinkResource { Rel = "view", Href = this.GetLocation(model) };
                yield return new LinkResource { Rel = "self", Href = this.GetLocation(model) };

                if (this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges))
                {
                    yield return new LinkResource { Rel = "edit", Href = this.GetLocation(model) };
                    yield return new LinkResource { Rel = "create", Href = this.GetLocation(model) };
                }
            }
        }
    }
}

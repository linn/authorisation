using Linn.Authorisation.Domain;

namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class PermissionResourceBuilder : IBuilder<Permission>
    {
        private readonly IAuthorisationService authService;

        public PermissionResourceBuilder(
            IAuthorisationService authService)
        {
            this.authService = authService;
        }

        public object Build(Permission model, IEnumerable<string> claims)
        {
            var privileges = claims == null ? new List<string>() : claims.ToList();
            var department = model.Privilege.Name.Split('.')[0];

            if ((this.authService.HasPermissionFor($"{department}.admin", privileges) ||
                 this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges)) && model != null)
            {
                if (model is IndividualPermission)
                {

                    return new PermissionResource
                    {
                        GranteeUri = ((IndividualPermission)model).GranteeUri,
                        Privilege = model.Privilege.Name,
                        PrivilegeId = model.Privilege.Id,
                        Links = this.BuildLinks(model, claims).ToArray(),
                        Id = model.Id
                    };
                }

                return new PermissionResource
                {
                    Privilege = model.Privilege.Name,
                    PrivilegeId = model.Privilege.Id,
                    Links = this.BuildLinks(model, claims).ToArray(),
                    Id = model.Id,
                    GranteeGroupId = ((GroupPermission)model).GranteeGroup.Id,
                    GroupName = ((GroupPermission)model).GranteeGroup.Name
                };
            }

            return null;
        }

        public string GetLocation(Permission model)
        {
            return $"/authorisation/permissions/{model.Id}";
        }

        object IBuilder<Permission>.Build(Permission model, IEnumerable<string> claims) => this.Build(model, claims);

        private IEnumerable<LinkResource> BuildLinks(Permission model, IEnumerable<string> claims)
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

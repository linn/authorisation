namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;
    using System.Linq;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class GroupResourceBuilder : IBuilder<Group>
    {
        private readonly MemberResourceBuilder memberResourceBuilder = new();
        private readonly IAuthorisationService authService;

        public GroupResourceBuilder(
            IAuthorisationService authService)
        {
            this.authService = authService;
        }

        public object Build(Group model, IEnumerable<string> claims)
        {
            var privileges = claims == null ? new List<string>() : claims.ToList();
            var department = model.Name.Split('.')[0];

            if ((this.authService.HasPermissionFor($"{department}.admin", privileges) ||
                 this.authService.HasPermissionFor(AuthorisedAction.AuthorisationAdmin, privileges)) && model != null)
            {
                var members = model.Members;
                var membersResources = members
                    .Select(member => (MemberResource)this.memberResourceBuilder.Build(member, claims)).ToList();

                return new GroupResource
                {
                    Active = model.Active, Name = model.Name, Id = model.Id, Members = membersResources,
                    Links = this.BuildLinks(model, claims).ToArray()
                };
            }

            return null;
        }

        public string GetLocation(Group model)
        {
            return $"/authorisation/groups/{model.Id}";
        }

        object IBuilder<Group>.Build(Group model, IEnumerable<string> claims) => this.Build(model, claims);

        private IEnumerable<LinkResource> BuildLinks(Group model, IEnumerable<string> claims)
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
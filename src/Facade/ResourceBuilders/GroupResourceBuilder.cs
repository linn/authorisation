namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class GroupResourceBuilder : IBuilder<Group>

    {
        public object Build(Group model, IEnumerable<string> claims)
        {
            return new GroupResource { Active = model.Active, Name = model.Name, Id = model.Id };
        }

        public string GetLocation(Group model)
        {
            throw new System.NotImplementedException();
        }

        object IBuilder<Group>.Build(Group model, IEnumerable<string> claims) => this.Build(model, claims);
    }
}
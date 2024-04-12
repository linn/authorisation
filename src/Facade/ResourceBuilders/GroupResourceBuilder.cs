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
            var members = model.Members;
            var membersResources = new List<MemberResource>();

            foreach (var member in members)
            {
                membersResources.Add(member);
            }


            return new GroupResource { 
                                             Active = model.Active,
                                            Name = model.Name,
                                            Id = model.Id,
                                            Members = membersResources,

                                     };

        }

        public string GetLocation(Group model)
        {
            throw new System.NotImplementedException();
        }

        object IBuilder<Group>.Build(Group model, IEnumerable<string> claims) => this.Build(model, claims);
    }
}
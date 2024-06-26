﻿namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;


    public class GroupResourceBuilder : IBuilder<Group>

    {
        private readonly MemberResourceBuilder memberResourceBuilder = new MemberResourceBuilder();

        public object Build(Group model, IEnumerable<string> claims)
        {
            var members = model.Members;
            var membersResources = new List<MemberResource>();

            foreach (Member member in members)
            {
                membersResources.Add((MemberResource)this.memberResourceBuilder.Build(member , claims));
            }

            return new GroupResource { 
                                             Active = model.Active, 
                                             Name = model.Name, 
                                             Id = model.Id, 
                                             Members = membersResources
                                     };
        }

        public string GetLocation(Group model)
        {
            throw new System.NotImplementedException();
        }

        object IBuilder<Group>.Build(Group model, IEnumerable<string> claims) => this.Build(model, claims);
    }
}
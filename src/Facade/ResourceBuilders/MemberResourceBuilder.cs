namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class MemberResourceBuilder : IBuilder<Member>
    {
        public object Build(Member model, IEnumerable<string> claims)
        {
            if (model is IndividualMember member)
            {
                return new MemberResource { Id = member.Id, MemberUri = member.MemberUri };
            }

            throw new NotImplementedException();
        }

        public string GetLocation(Member model)
        {
            throw new System.NotImplementedException();
        }

        object IBuilder<Member>.Build(Member model, IEnumerable<string> claims) => this.Build(model, claims);
    }
}
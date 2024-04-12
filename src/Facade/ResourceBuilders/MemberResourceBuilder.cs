namespace Linn.Authorisation.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class MemberResourceBuilder : IBuilder<IndividualMember>
    {

        public object Build(IndividualMember model, IEnumerable<string> claims)
        {
            return new MemberResource { MemberUri = model.MemberUri };
        }

        public string GetLocation(IndividualMember model)
        {
            throw new System.NotImplementedException();
        }
        object IBuilder<IndividualMember>.Build(IndividualMember model, IEnumerable<string> claims) => this.Build(model, claims);

    }
}
namespace Linn.Authorisation.Facade.ResourceBuilders;
{
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class MemberResourceBuilder : IBuilder<Member>
    {

        public MemberResource Build(MemberResource model)
        {
            return new MemberResource { MemberUri = model.MemberUri, };
        }

        object IResourceBuilder<Member>.Build(Member model) => this.Build(model);


    }
}
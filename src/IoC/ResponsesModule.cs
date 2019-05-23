namespace Linn.Authorisation.IoC
{
    using Autofac;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Common.Facade;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PrivilegeResourceBuilder>().As<IResourceBuilder<Privilege>>();
        }
    }
}
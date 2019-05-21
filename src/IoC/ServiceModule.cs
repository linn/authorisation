namespace Linn.Authorisation.IoC
{
    using Autofac;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<GroupService>().As<IGroupService>();

            // facade services
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();
        }
    }
}

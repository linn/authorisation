namespace Linn.Authorisation.IoC
{
    using Autofac;

    using Linn.Authorisation.Facade;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();
        }
    }
}

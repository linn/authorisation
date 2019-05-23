namespace Linn.Authorisation.IoC
{
    using Autofac;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<GroupService>().As<IGroupService>();

            // facade services
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();
            builder.RegisterType<PrivilegeFacadeService>()
                .As<IFacadeService<PrivilegeService, int, PrivilegeResource, PrivilegeResource>>();
            builder.RegisterType<IPermissionService>().As<PermissionService>();
        }
    }
}

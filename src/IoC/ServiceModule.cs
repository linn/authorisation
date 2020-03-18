namespace Linn.Authorisation.IoC
{
    using Autofac;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Resources;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;

    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // domain services
            builder.RegisterType<PrivilegeService>().As<IPrivilegeService>();
            builder.RegisterType<PermissionService>().As<IPermissionService>();

            // facade services
            builder.RegisterType<MemberPrivilegesService>().As<IMemberPrivilegesService>();
            builder.RegisterType<PrivilegeFacadeService>()
                .As<IPrivilegeFacadeService>();
            builder.RegisterType<PermissionService>().As<IPermissionService>();
            builder.RegisterType<GroupService>()
                .As<IGroupService>();
            builder.RegisterType<AuthorisationService>().As<IAuthorisationService>();
        }
    }
}

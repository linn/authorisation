namespace Linn.Authorisation.IoC
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.Resources;
    using Linn.Common.Facade;
    using Linn.Common.Pdf;

    using Microsoft.Extensions.DependencyInjection;

    using RazorEngineCore;


    public static class ServiceExtensions
    {
        public static IServiceCollection AddFacade(this IServiceCollection services)
        {
            return services.AddTransient<IBuilder<Privilege>, PrivilegeResourceBuilder>()
                .AddTransient<IBuilder<Permission>, PermissionResourceBuilder>()
                .AddTransient<IBuilder<Group>, GroupResourceBuilder>()
                .AddTransient<IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource>, PrivilegeFacadeService>()
                .AddTransient<IFacadeResourceService<Group, int, GroupResource, GroupResource>, GroupFacadeService>()
                .AddTransient<IPermissionFacadeService, PermissionFacadeService>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IRazorEngine, RazorEngine>()
                .AddTransient<ITemplateEngine, RazorTemplateEngine>()
                .AddTransient<IPermissionService, PermissionService>();
        }
    }
}

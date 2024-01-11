namespace Linn.Authorisation.IoC
{
    using Linn.Authorisation.Domain;
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
                .AddTransient<IFacadeResourceService<Privilege, int, PrivilegeResource, PrivilegeResource>, PrivilegeFacadeService>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IRazorEngine, RazorEngine>()
                .AddTransient<ITemplateEngine, RazorTemplateEngine>();
        }
    }
}

namespace Linn.Authorisation.IoC
{
    using System.Collections.Generic;

    using Linn.Authorisation.Resources;
    using Linn.Common.Service.Core.Handlers;

    using Microsoft.Extensions.DependencyInjection;

    public static class HandlerExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services.AddTransient<IHandler, JsonResultHandler<ProcessResultResource>>()
                .AddTransient<IHandler, JsonResultHandler<IEnumerable<PrivilegeResource>>>()
                .AddTransient<IHandler, JsonResultHandler<PrivilegeResource>>()
                .AddTransient<IHandler, JsonResultHandler<IEnumerable<PermissionResource>>>()
                .AddTransient<IHandler, JsonResultHandler<PermissionResource>>()
                .AddTransient<IHandler, JsonResultHandler<IEnumerable<GroupResource>>>()
                .AddTransient<IHandler, JsonResultHandler<GroupResource>>();
        }
    }
}

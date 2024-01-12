namespace Linn.Authorisation.IoC
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Persistence;
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services.AddScoped<ServiceDbContext>().AddTransient<DbContext>(a => a.GetService<ServiceDbContext>())
                .AddTransient<ITransactionManager, TransactionManager>()
                .AddTransient<IRepository<Privilege, int>, EntityFrameworkRepository<Privilege, int>>(
                    r => new EntityFrameworkRepository<Privilege, int>(r.GetService<ServiceDbContext>()?.Privileges))
                .AddTransient<IRepository<Permission, int>, PermissionsRepository>()
                .AddTransient<IRepository<Group, int>, GroupRepository>();

        }
    }
}

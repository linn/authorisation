namespace Linn.Authorisation.IoC
{
    using Autofac;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Persistence;
    using Linn.Common.Persistence;
    using Linn.Common.Persistence.EntityFramework;

    using Microsoft.EntityFrameworkCore;

    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceDbContext>().AsSelf().As<DbContext>().InstancePerRequest();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();
            builder.RegisterType<PermissionsRepository>().As<IRepository<Permission, int>>();
            builder.RegisterType<PrivilegeRepository>().As<IRepository<Privilege, int>>();
            builder.RegisterType<GroupRepository>().As<IRepository<Group, int>>();
        }
    }
}

namespace Linn.Authorisation.IoC
{
    using System.Collections.Generic;

    using Autofac;
    using Domain.Groups;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Common.Facade;
    using Linn.Production.Facade.ResourceBuilders;

    public class ResponsesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PrivilegeResourceBuilder>().As<IResourceBuilder<Privilege>>();
            builder.RegisterType<PrivilegesResourceBuilder>().As<IResourceBuilder<IEnumerable<Privilege>>>();
            builder.RegisterType<PermissionResourceBuilder>().As<IResourceBuilder<Permission>>();
            builder.RegisterType<PermissionsResourceBuilder>().As<IResourceBuilder<IEnumerable<Permission>>>();
            builder.RegisterType<GroupResourceBuilder>().As<IResourceBuilder<Group>>();
            builder.RegisterType<GroupsResourceBuilder>().As<IResourceBuilder<IEnumerable<Group>>>();
            builder.RegisterType<ErrorResourceBuilder>().As<IResourceBuilder<Error>>();
        }
    }
}
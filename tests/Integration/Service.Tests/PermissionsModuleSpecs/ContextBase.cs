namespace Linn.Permission.Service.Tests.PermissionsModuleSpecs
{
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Service;
    using Linn.Authorisation.Service.ResponseProcessors;
    using Linn.Authorisation.Service.Tests;
    using Linn.Common.Facade;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IPermissionService PermissionService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PermissionService = Substitute.For<IPermissionService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PermissionService);
                        with.ResponseProcessor<PermissionResponseProcessor>();
                        with.Module<PermissionsModule>();
                        with.Dependency<IResourceBuilder<Permission>>(new PermissionResourceBuilder());
                    });

            this.Browser = new Browser(bootstrapper);
        }

    }
}
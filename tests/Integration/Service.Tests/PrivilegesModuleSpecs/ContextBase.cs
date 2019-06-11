namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Modules;
    using Linn.Authorisation.Service.ResponseProcessors;
    using Linn.Authorisation.Service.Tests;
    using Linn.Common.Facade;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;
    using System.Collections.Generic;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<Privilege, int, PrivilegeResource, PrivilegeResource> PrivilegeService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PrivilegeService = Substitute.For<IFacadeService<Privilege, int, PrivilegeResource, PrivilegeResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PrivilegeService);
                        with.Module<PrivilegesModule>();
                        with.ResponseProcessor<PrivilegeResponseProcessor>();
                        with.Dependency<IResourceBuilder<Privilege>>(new PrivilegeResourceBuilder());
                        with.ResponseProcessor<PrivilegesResponseProcessor>();
                        with.Dependency<IResourceBuilder<IEnumerable<Privilege>>>(new PrivilegesResourceBuilder());
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}

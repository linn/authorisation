namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Modules;
    using Linn.Authorisation.Service.Tests;
    using Linn.Common.Facade;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

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
                    });

            this.Browser = new Browser(bootstrapper);
        }

    }
}

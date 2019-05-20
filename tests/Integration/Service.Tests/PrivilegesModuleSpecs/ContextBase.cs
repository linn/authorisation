namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Service.Modules;

    using Nancy.Testing;

    using NSubstitute;

    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.AuthorisationService);
                        with.Module<PrivilegesModule>();
                    });

            this.Browser = new Browser(bootstrapper);
        }
            
    }
}

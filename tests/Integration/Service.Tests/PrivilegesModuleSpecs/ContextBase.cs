namespace Linn.Authorisation.Service.Tests.PrivilegesModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Modules;
    using Linn.Authorisation.Service.ResponseProcessors;
    using Linn.Authorisation.Service.Tests;
    using Linn.Common.Authorisation;
    using Linn.Common.Facade;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;

    public abstract class ContextBase : NancyContextBase
    {
        protected IPrivilegeFacadeService PrivilegeService { get; set; }

        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PrivilegeService = Substitute.For<IPrivilegeFacadeService>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PrivilegeService);
                        with.Dependency(this.AuthorisationService);
                        with.Module<PrivilegesModule>();
                        with.ResponseProcessor<PrivilegeResponseProcessor>();
                        with.Dependency<IResourceBuilder<Privilege>>(new PrivilegeResourceBuilder());
                        with.ResponseProcessor<PrivilegesResponseProcessor>();
                        with.Dependency<IResourceBuilder<IEnumerable<Privilege>>>(new PrivilegesResourceBuilder()); with.RequestStartup(
                            (container, pipelines, context) =>
                                {
                                    var claims = new List<Claim>
                                                     {
                                                         new Claim(ClaimTypes.Role, "employee"),
                                                         new Claim(ClaimTypes.NameIdentifier, "test-user")
                                                     };

                                    var user = new ClaimsIdentity(claims, "jwt");

                                    context.CurrentUser = new ClaimsPrincipal(user);
                                });
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}

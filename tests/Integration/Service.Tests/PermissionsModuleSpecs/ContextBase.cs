namespace Linn.Authorisation.Service.Tests.PermissionsModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Facade.ResourceBuilders;
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
        protected IPermissionService PermissionService { get; set; }

        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.PermissionService = Substitute.For<IPermissionService>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    {
                        with.Dependency(this.PermissionService);
                        with.Dependency(this.AuthorisationService);
                        with.Module<PermissionsModule>();
                        with.ResponseProcessor<PermissionResponseProcessor>();
                        with.ResponseProcessor<PermissionsResponseProcessor>();
                        with.Dependency<IResourceBuilder<IEnumerable<Permission>>>(new PermissionsResourceBuilder());
                        // with.Module<GroupModule>();
                        //with.ResponseProcessor<GroupResponseProcessor>();
                        //with.Dependency<IResourceBuilder<Group>>(new GroupResourceBuilder());
                        with.Dependency<IResourceBuilder<Permission>>(new PermissionResourceBuilder()); with.RequestStartup(
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
namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Authorisation.Facade;
    using Authorisation.Facade.ResourceBuilders;
    using Common.Facade;
    using Domain;
    using Domain.Groups;

    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Authorisation;

    using Modules;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;
    using ResponseProcessors;

    public abstract class ContextBase : NancyContextBase
    {
        protected IGroupService GroupService { get; set; }

        protected IPermissionFacadeService PermissionService{ get; set; }

        protected IAuthorisationService AuthorisationService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.GroupService = Substitute.For<IGroupService>();
            this.PermissionService = Substitute.For<IPermissionFacadeService>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.GroupService);
                    with.Dependency(this.PermissionService);
                    with.Dependency(this.AuthorisationService);
                    with.Module<GroupModule>();
                    with.ResponseProcessor<GroupResponseProcessor>();
                    with.Dependency<IResourceBuilder<Group>>(new GroupResourceBuilder());
                    with.ResponseProcessor<GroupsResponseProcessor>();
                    with.Dependency<IResourceBuilder<IEnumerable<Group>>>(new GroupsResourceBuilder());
                    with.ResponseProcessor<PermissionResponseProcessor>();
                    with.Dependency<IResourceBuilder<Permission>>(new PermissionResourceBuilder());
                    with.ResponseProcessor<PermissionsResponseProcessor>();
                    with.Dependency<IResourceBuilder<IEnumerable<Permission>>>(new PermissionsResourceBuilder()); with.RequestStartup(
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

namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using Authorisation.Facade.ResourceBuilders;
    using Common.Facade;
    using Domain;
    using Domain.Groups;
    using Modules;
    using Nancy.Testing;
    using NSubstitute;
    using NUnit.Framework;
    using Resources;
    using ResponseProcessors;

    public abstract class ContextBase : NancyContextBase
    {
        protected IFacadeService<Group, int, GroupResource, GroupResource> GroupService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.GroupService = Substitute.For<IFacadeService<Group, int, GroupResource, GroupResource>>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.GroupService);
                    with.Module<GroupModule>();
                    with.ResponseProcessor<GroupResponseProcessor>();
                    with.Dependency<IResourceBuilder<Group>>(new GroupResourceBuilder());
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}

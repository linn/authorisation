namespace Linn.Authorisation.Service.Tests.GroupModuleSpecs
{
    using System.Collections.Generic;

    using Authorisation.Facade;
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
        protected IGroupService GroupService { get; set; }

        [SetUp]
        public void EstablishContext()
        {
            this.GroupService = Substitute.For<IGroupService>();

            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                {
                    with.Dependency(this.GroupService);
                    with.Module<GroupModule>();
                    with.ResponseProcessor<GroupResponseProcessor>();
                    with.Dependency<IResourceBuilder<Group>>(new GroupResourceBuilder());
                    with.ResponseProcessor<GroupsResponseProcessor>();
                    with.Dependency<IResourceBuilder<IEnumerable<Group>>>(new GroupsResourceBuilder());
                });

            this.Browser = new Browser(bootstrapper);
        }
    }
}

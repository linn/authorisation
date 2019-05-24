﻿namespace Linn.Authorisation.Service.Tests.AuthorisationModuleSpecs
{
    using System.Collections.Generic;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Service.Modules;
    using Linn.Authorisation.Service.ResponseProcessors;
    using Linn.Common.Facade;

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
                        with.ResponseProcessor<PrivilegesResponseProcessor>();
                        with.Module<AuthorisationModule>();
                        with.Dependency<IResourceBuilder<Privilege>>(new PrivilegeResourceBuilder());

                        with.Dependency<IResourceBuilder<IEnumerable<Privilege>>>(new PrivilegesResourceBuilder());
                    });

            this.Browser = new Browser(bootstrapper);
        }
    }
}

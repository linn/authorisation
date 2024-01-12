namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Net.Http;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.IoC;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Modules;
    using Linn.Common.Facade;
    using Linn.Common.Logging;
    using Linn.Common.Persistence;

    using Microsoft.Extensions.DependencyInjection;

    using NSubstitute;

    using NUnit.Framework;

    public class ContextBase
    {
        protected HttpClient Client { get; set; }

        protected HttpResponseMessage Response { get; set; }
        
        protected IPermissionFacadeService FacadeService { get; private set; }

        protected ILog Log { get; private set; }
        
        protected IPermissionService DomainService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DomainService = Substitute.For<IPermissionService>();
            this.FacadeService = new PermissionFacadeService(
                this.DomainService,
                new PermissionResourceBuilder());
            this.Log = Substitute.For<ILog>();

            this.Client = TestClient.With<PermissionsModule>(
                services =>
                    {
                        services.AddSingleton(this.FacadeService);
                        services.AddSingleton(this.Log);
                        services.AddHandlers();
                        services.AddRouting();
                    },
                FakeAuthMiddleware.EmployeeMiddleware);
        }
    }
}
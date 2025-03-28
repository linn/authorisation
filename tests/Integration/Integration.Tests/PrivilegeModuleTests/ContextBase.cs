namespace Linn.Authorisation.Integration.Tests.PrivilegeModuleTests
{
    using System.Net.Http;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.IoC;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Modules;
    using Linn.Common.Authorisation;
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

        protected IPrivilegeFacadeService FacadeService { get; private set; }

        protected ITransactionManager TransactionManager { get; set; }

        protected IPrivilegeService DomainService { get; private set; }

        protected ILog Log { get; private set; }

        protected IRepository<Privilege, int> PrivilegeRepository { get; private set; }

        protected IAuthorisationService AuthService { get; private set; }

        protected IPrivilegeService Sut { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DomainService = Substitute.For<IPrivilegeService>();
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.PrivilegeRepository = Substitute.For<IRepository<Privilege, int>>();
            this.AuthService = Substitute.For<IAuthorisationService>();

            this.FacadeService = new PrivilegeFacadeService(
                this.PrivilegeRepository,
                this.DomainService,
                new PrivilegeResourceBuilder(),
                this.TransactionManager,
                this.AuthService
                );
            this.Log = Substitute.For<ILog>();
            this.Sut = new PrivilegeService(this.PrivilegeRepository);

            this.Client = TestClient.With<PrivilegeModule>(
                services =>
                    {
                        services.AddSingleton(this.TransactionManager);
                        services.AddSingleton(this.FacadeService);
                        services.AddSingleton(this.Log);
                        services.AddHandlers();
                        services.AddRouting();
                    },
                FakeAuthMiddleware.EmployeeMiddleware);
        }
    }
}

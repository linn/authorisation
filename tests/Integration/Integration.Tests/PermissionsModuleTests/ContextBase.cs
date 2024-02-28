namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System.Net.Http;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Domain.Services;
    using Linn.Authorisation.Facade.ResourceBuilders;
    using Linn.Authorisation.Facade.Services;
    using Linn.Authorisation.IoC;
    using Linn.Authorisation.Service.Modules;
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

        protected IRepository<Permission, int> PermissionRepository { get; private set; }

        protected IRepository<Privilege, int> PrivilegeRepository { get; private set; }

        public IRepository<Group, int> GroupRespository { get; private set; }

        protected ITransactionManager TransactionManager { get; set; }

        [SetUp]
        public void SetUpContext()
        {
            this.DomainService = Substitute.For<IPermissionService>();
            this.PermissionRepository = Substitute.For<IRepository<Permission, int>>();
            this.PrivilegeRepository = Substitute.For<IRepository<Privilege, int>>();
            this.GroupRespository = Substitute.For<IRepository<Domain.Groups.Group, int>>();
            this.TransactionManager = Substitute.For<ITransactionManager>();

            this.FacadeService = new PermissionFacadeService(
                this.DomainService,
                new PermissionResourceBuilder(),
                this.PermissionRepository,
                this.PrivilegeRepository,
                this.GroupRespository,
                this.TransactionManager);
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

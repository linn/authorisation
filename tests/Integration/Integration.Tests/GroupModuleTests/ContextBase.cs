namespace Linn.Authorisation.Integration.Tests.GroupModuleTests
{
    using System.Net.Http;

    using Linn.Authorisation.Domain.Groups;
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

        protected ITransactionManager TransactionManager { get; set; }

        protected IFacadeResourceService<Group, int, GroupResource, GroupResource> FacadeService { get; private set; }

        protected ILog Log { get; private set; }

        protected IRepository<Group, int> GroupRepository { get; private set; }

        protected IMembersFacadeService MembersFacadeService { get; private set; }

        protected IAuthorisationService AuthorisationService { get; private set; }

        protected IGroupService GroupService { get; private set; }

        [SetUp]
        public void SetUpContext()
        {
            this.TransactionManager = Substitute.For<ITransactionManager>();
            this.GroupRepository = Substitute.For<IRepository<Group, int>>();
            this.AuthorisationService = Substitute.For<IAuthorisationService>();
            this.GroupService = Substitute.For<IGroupService>();

            this.FacadeService = new GroupFacadeService(
                this.GroupRepository,
                this.TransactionManager,
                new GroupResourceBuilder(),
                this.GroupRepository,
                this.AuthorisationService,
                this.GroupService);

            this.MembersFacadeService = new MembersFacadeService(
                this.GroupRepository,
                this.TransactionManager);

            this.Log = Substitute.For<ILog>();
            this.MembersFacadeService = new MembersFacadeService(this.GroupRepository, this.TransactionManager);

            this.Client = TestClient.With<GroupModule>(
                services =>
                {
                    services.AddSingleton(this.TransactionManager);
                    services.AddSingleton(this.FacadeService);
                    services.AddSingleton(this.Log);
                    services.AddHandlers();
                    services.AddRouting();
                    services.AddSingleton(this.MembersFacadeService);
                },
                FakeAuthMiddleware.EmployeeMiddleware);
        }
    }
}

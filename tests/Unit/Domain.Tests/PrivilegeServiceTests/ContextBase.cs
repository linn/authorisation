namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
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
    using Linn.Authorisation.Domain.Services;
    using NSubstitute;
    using Linn.Common.Persistence;
    using NUnit.Framework;

    public abstract class ContextBase
    {
        protected IPrivilegeService Sut { get; private set; }

        protected IRepository<Permission, int> PermissionRepository
        {
            get;
            private set;
        }
        protected IRepository<Group, int> GroupRepository
        {
            get;
            private set;
        }

        [SetUp]
        public void SetUpContext()
        {
            this.PrivilegeRepository =
                Substitute.For<IRepository<Privilege, int>>();
            this.GroupRepository =
                Substitute.For<IRepository<Group, int>>();

            this.Sut = new PrivilegeService(this.GroupRepository, this.PrivilegeRepository);
        }
    }
}
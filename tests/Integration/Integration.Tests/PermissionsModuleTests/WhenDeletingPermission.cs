using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingPermission : ContextBase
    {
        [SetUp]
        public void SetUp()
        {
            this.DomainService.DeletePermission(100).Returns(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "1" },
                                Id = 100
                            },
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "2" },
                                Id = 101
                                
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions?who=100",
                with =>
                    {
                        with.Accept("application/json");
                    }).Result;
        }
    }
}

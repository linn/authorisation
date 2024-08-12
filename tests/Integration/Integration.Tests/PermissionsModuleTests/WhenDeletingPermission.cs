namespace Linn.Authorisation.Integration.Tests.PermissionsModuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Authorisation.Integration.Tests.Extensions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenDeletingPermission : ContextBase
    {
        private Group group;

        [SetUp]
        public void SetUp()
        {
            this.group = new Group
                             {
                                 Id = 2,
                                 Name = "testing-get-group"
                             };
            this.group.AddIndividualMember("/employees/2", "/1");

            this.PermissionRepository.Remove(1).(
                new List<Permission>
                    {
                        new IndividualPermission
                            {
                                Privilege = new Privilege { Name = "1", Id = 1 }, GranteeUri = "/employees/1",
                                Id = 1
                            },
                        new GroupPermission
                            {
                                Privilege = new Privilege { Name = "1", Id = 1 },
                                Id = 2,
                                GranteeGroup = this.group
                            }
                    }.AsQueryable());

            this.Response = this.Client.Get(
                "/authorisation/permissions/privilege?privilegeId=1",
                with => { with.Accept("application/json"); }).Result;
        }
    }
}

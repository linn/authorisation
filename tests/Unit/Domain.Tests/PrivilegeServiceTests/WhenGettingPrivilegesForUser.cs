namespace Linn.Authorisation.Domain.Tests.PrivilegeServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Linn.Authorisation.Domain;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingPrivilegesForUser : ContextBase
    {
        private IEnumerable<Privilege> result;

        [SetUp]
        public void SetUp()
        {
            var priv1 = new Privilege { Name = "click-buttons.admin" };

            var priv2 = new Privilege { Name = "type-things.admin" };

            //var privileges = new List<Privilege> { priv1, priv2 };

            var indvPermissions = new List<IndividualPermission>
                                      {
                                          new IndividualPermission("/employees/33087", priv1, "/employees/7004"),
                                          new IndividualPermission("/employees/33087", priv2, "/employees/7004"),
                                      };

                //var groups = new List<Group>
                //                 {
                //                     new Group
                //                         {
                //                            Name = "do-anything",
                //                            Active = true,
                //                            Members = new List<Member>
                //                                          new IndividualMember { 
                //                                          MemberUri = "/employees/33087",
                //                                          AddedByUri = "/employees/12345",
                //                                          DateAdded = new DateTime()
                //                                        },
                //                     new IndividualMember {
                //                                                  MemberUri = "not-the-right-member",
                //                                                  AddedByUri = "/employees/12345",
                //                                                  DateAdded = new DateTime()
                //                                              }
                //                     }
                //    };



            this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
                    .Returns(indvPermissions);

            this.GroupRepository.FindAll()
                    .Returns(new List<Group>());


            //this.PermissionRepository.FilterBy(Arg.Any<Expression<Func<Permission, bool>>>())
            //    .Returns(groupPermissions);
            //to test a case where we have groups, we need to do the above,
            //how do? ^ we've already specified it returns one thing, je suis confuse

            this.result = this.Sut.GetPrivileges("/employees/33087");
            }

            [Test]
            public void ShouldReturnPrivilegs()
            {
                this.result.Count().Should().Be(2);
                this.result.FirstOrDefault(
                    x => x.Name == "click-buttons.admin").Should().NotBeNull();
                this.result.FirstOrDefault(
                    x => x.Name == "type-things.admin").Should().NotBeNull();
        }

            [Test]
            public void ShouldCallPermissionsRepository()
            {
                this.PermissionRepository.Received().FilterBy(Arg.Any<Expression<Func<Permission, bool>>>());
            }

            [Test]
            public void ShouldCallGroupRepository()
            {
                this.GroupRepository.Received().FindAll());
            }
        }
}

namespace Linn.Authorisation.Service.Tests.Facade.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Groups;
    using Domain.Permissions;

    public static class TestDbContext
    {
        public static List<Privilege> Privileges { get; set; }

        public static List<Permission> Permissions { get; set; }

        public static List<Group> Groups { get; set; }

        public static void SetUp()
        {
            Privileges = new List<Privilege>();
            Permissions = new List<Permission>();
            Groups = new List<Group>();
        }

        public static void BuildPrivilege(string name)
        {
            Privileges.Add(new Privilege(name));
        }

        public static void BuildPermission(string granteeUri, string privilegeName)
        {
            var privilege = Privileges.SingleOrDefault(p => p.Name == privilegeName);
            Permissions.Add(new IndividualPermission(granteeUri, privilege, DateTime.UtcNow, "/employees/7004"));
        }

        public static void BuildPermission(Group group, string privilegeName)
        {
            var privilege = Privileges.SingleOrDefault(p => p.Name == privilegeName);
            Permissions.Add(new GroupPermission(group, privilege, DateTime.UtcNow, "/employees/7004"));
        }

        public static Group BuildGroup(string groupName, bool active)
        {
            var group = new Group(groupName, active);
            Groups.Add(group);
            return group;
        }

        public static Group BuildGroupMember(this Group group, string memberUri)
        {
            group.AddIndividualMember(memberUri, "/employees/7004");
            return group;
        }

        public static Group BuildGroupMember(this Group group, Group memberGroup)
        {
            group.AddGroupMember(memberGroup, "/employees/7004");
            return group;
        }
    }
}

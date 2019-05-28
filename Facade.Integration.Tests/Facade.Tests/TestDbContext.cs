namespace Facade.Integration.Tests.Facade.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

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

        public static void BuildPrivilege(string name, bool active = true)
        {
            Privileges.Add(new Privilege(name, active));
        }

        public static void BuildPermission(string granteeUri, string privilegeName)
        {
            var privilege = Privileges.SingleOrDefault(p => p.Name == privilegeName);
            Permissions.Add(new IndividualPermission(granteeUri, privilege, "/employees/7004"));
        }

        public static void BuildPermission(Group group, string privilegeName)
        {
            var privilege = Privileges.SingleOrDefault(p => p.Name == privilegeName);
            Permissions.Add(new GroupPermission(group, privilege, "/employees/7004"));
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

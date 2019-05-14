namespace Linn.Authorisation.Integration.Tests.Facade.Tests
{
    using System.Collections.Generic;
    using Domain;

    public static class TestDbContext
    {
        public static List<Role> Roles { get; set; }

        public static void SetUp()
        {
            Roles = new List<Role>();
        }
    }
}

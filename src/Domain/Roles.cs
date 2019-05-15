namespace Linn.Authorisation.Domain
{
    using System.Collections.Generic;

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Permission> Permissions { get; set; }

        public IEnumerable<string> Members { get; set; }
    }
}

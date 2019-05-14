namespace Linn.Authorisation.Domain
{
    using System.Collections.Generic;

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Claim> Claims { get; set; }

        public IEnumerable<string> Members { get; set; }
    }
}

namespace Linn.Authorisation.Domain
{
    using System;
    using System.Collections.Generic;

    public class Claim
    {
        public string Name { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Claim> Claims { get; set; }

        public IEnumerable<Uri> Members { get; set; }
    }
}

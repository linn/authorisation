namespace Linn.Authorisation.Domain
{
    using System.Collections.Generic;

    public class Privilege : Entity
    {
        public Privilege(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
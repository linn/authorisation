namespace Linn.Authorisation.Domain
{
    using System.Collections.Generic;

    public class Privilege : Entity
    {
        public Privilege(string name, bool active = true)
        {
            this.Name = name;
            this.Active = active;
        }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
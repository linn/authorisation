namespace Linn.Authorisation.Domain
{
    public class Privilege : Entity
    {
        public Privilege(string name, bool active = true)
        {
            this.Name = name;
            this.Active = active;
        }

        public Privilege()
        {
            // empty args constructor needed for ef      
        }

        public string Name { get; set; }

        public bool Active { get; set; }

        public void Update(string name, bool active)
        {
            this.Name = name;
            this.Active = active;
        }
    }
}

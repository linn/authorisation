namespace Linn.Authorisation.Domain
{
    public class Claim
    {
        public Claim(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}

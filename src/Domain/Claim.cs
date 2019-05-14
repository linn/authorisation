namespace Linn.Authorisation.Domain
{
    using System.Collections.Generic;

    public class Claim
    {
        public Claim(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }


}

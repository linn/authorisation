namespace Linn.Authorisation.Domain.Permissions
{
    using System;

    public abstract class Permission : Entity
    {
        protected Permission()
        {
            // empty args constructor needed for ef
        }

        protected Permission(Privilege privilege, DateTime dateGranted, string grantedByUri)
        {
            this.Privilege = privilege;
            this.DateGranted = DateTime.UtcNow;
            this.GrantedByUri = grantedByUri;
        }

        public Privilege Privilege { get; set; }

        public DateTime DateGranted { get; set; }

        public string GrantedByUri { get; set; }
    }
}

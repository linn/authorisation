namespace Linn.Authorisation.Domain.Permissions
{
    using System;

    public abstract class Permission : Entity
    {
        protected Permission(Privilege privilege, DateTime dateGranted, string grantedByUri)
        {
            this.Privilege = privilege;
            this.DateGranted = dateGranted;
            this.GrantedByUri = grantedByUri;
        }

        public Privilege Privilege { get; set; }

        public DateTime DateGranted { get; set; }

        public string GrantedByUri { get; set; }
    }
}
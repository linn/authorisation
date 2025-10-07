namespace Linn.Authorisation.Domain.Permissions
{
    using System;
    using System.Collections.Generic;

    using Linn.Authorisation.Domain.Exceptions;

    public abstract class Permission : Entity
    {
        protected Permission()
        {
            // empty args constructor needed for ef
        }

        protected Permission(Privilege privilege, string grantedByUri)
        {
            this.Privilege = privilege;
            this.DateGranted = DateTime.UtcNow;
            this.GrantedByUri = grantedByUri;
        }

        public Privilege Privilege { get; set; }

        public DateTime DateGranted { get; set; }

        public string GrantedByUri { get; set; }

        public bool CheckActive()
        {
            if (this.Privilege.Active == false)
            {
                return false;
            }

            return true;
        }
    }
}

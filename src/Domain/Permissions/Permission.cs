namespace Linn.Authorisation.Domain.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.Design;
    using System.Linq;

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

        

    }
}
    
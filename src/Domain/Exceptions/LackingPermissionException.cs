namespace Linn.Authorisation.Domain.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class LackingPermissionException : DomainException
    {
        public LackingPermissionException(string message)
            : base(message)
        {
        }

        public LackingPermissionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

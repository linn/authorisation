namespace Linn.Authorisation.Domain.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class InactivePrivilegeException : DomainException
    {
        public InactivePrivilegeException(string message)
            : base(message)
        {
        }

        public InactivePrivilegeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


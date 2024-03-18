namespace Linn.Authorisation.Domain.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class DuplicatePrivilegeNameException : DomainException
    {
        public DuplicatePrivilegeNameException(string message)
            : base(message)
        {
        }

        public DuplicatePrivilegeNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

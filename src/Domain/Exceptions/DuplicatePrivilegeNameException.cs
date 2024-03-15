using Linn.Common.Domain.Exceptions;
using System;

namespace Linn.Authorisation.Domain.Exceptions
{
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

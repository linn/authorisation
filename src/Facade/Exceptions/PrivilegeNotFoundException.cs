namespace Linn.Authorisation.Facade.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class PrivilegeNotFoundException : DomainException
    {
        public PrivilegeNotFoundException(string message)
            : base(message)
        {
        }

        public PrivilegeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}